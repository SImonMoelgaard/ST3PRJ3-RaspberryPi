using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
using DataAccessLogic;
using DTO_s;

namespace BusinessLogic
{
    /// <summary>
    /// businesscontrolleren. herfra går kommunikation fra presentationlaget til datalaget. Det er også herfra al kommunikation til busineslagets klasser sker.
    /// </summary>
    public class BusinessController : UdpProvider
    {
        public double CalibrationValue { get; set; }
        private bool AlarmOn { get; set; }
        public bool _startMonitoring;
        private DTO_BP Bp;
        private DTO_Calculated calculated;
        private DTO_ExceededVals exceededVals;
        private ZeroAdjustment zeroAdjust;
        public DataController dataControllerObj;
        private Processing processing;
        private Compare compare;
        private Calibration calibration;
        private BatteryStatus batteryStatus;
        private double zeroAdjustMean;
        private double calibrationMean;
        private bool _systemOn;
        private DTO_ExceededVals limitValExceeded;
        public string CommandsPc { get; private set; }
        public DTO_LimitVals LimitVals { get; private set; }
        private bool ledOn;
        private List<double> bpList;
        private string highSys;
        private string lowMean;


        private readonly BlockingCollection<DataContainerUdp> _dataQueueCommand;
        private readonly BlockingCollection<DataContainerMeasureVals> _dataQueueMeasure;
        private readonly BlockingCollection<DataContainerUdp> _dataQueueLimit;

        /// <summary>
        /// contructor for datacontrolleren. herigennem bliver de forskellige atributter og objekter oprettet
        /// </summary>
        /// <param name="dataQueueMeasure">datakø til blodtryksmålinger</param>
        /// <param name="dataQueueLimit"> datakø til grænseværdis DTO'er fra UI</param>
        /// <param name="dataQueueCommands"> datakø til comandoer fra UI</param>
        public BusinessController(BlockingCollection<DataContainerUdp> dataQueueCommand, BlockingCollection<DataContainerUdp> dataQueueLimit, BlockingCollection<DataContainerMeasureVals> dataQueueMeasure)
        {
            _dataQueueCommand = dataQueueCommand;
            _dataQueueMeasure = dataQueueMeasure;
            _dataQueueLimit = dataQueueLimit;
            ledOn = false;
            dataControllerObj = new DataController(_dataQueueMeasure, _dataQueueLimit, _dataQueueCommand);
            bpList = new List<double>(546);
            batteryStatus = new BatteryStatus();
            calibration = new Calibration();
            compare = new Compare();
            processing = new Processing();
            zeroAdjust = new ZeroAdjustment();
        }
        /// <summary>
        /// kalder metoden startUdpLimit på datalaget
        /// </summary>
        public void StartProducerLimit()
        {
           dataControllerObj.StartUdpLimit();
        }
        /// <summary>
        /// kalder metoden udpListenerListen på datalaget
        /// </summary>
        public void StartProducerCommands()
        {
            dataControllerObj.UdpListenerListen();
        }
        /// <summary>
        /// indikere om systemet er tændt
        /// </summary>
        /// <param name="systemOn"> bool der indikere om systmet er tændt</param>
        public void SetSystemOn(bool systemOn)
        {
            _systemOn = systemOn;
            dataControllerObj.ReceiveSystemOn(_systemOn);
        }
        /// <summary>
        /// Metode der modtager bool fra presentationController og sender den videre til dataController
        /// </summary>
        /// <param name="startMonitoring"> bool til indikation på om systemet skal foretage en monitorering </param>
        public void SetStartMonitoring(bool startMonitoring)
        {
            _startMonitoring = startMonitoring;
            dataControllerObj.ReceiveStartMonitoring(_startMonitoring);
        }
     
        /// <summary>
        /// consumer på commands. hiver commands ud af datakøen til commands, så vi kan gå "mod lagenes retning". notifyer samtidig presentationcontrolleren(observeren) om at der ny data
        /// </summary>
        public void RunCommands()
        {
            while (!_dataQueueCommand.IsCompleted)
            {
                try
                {
                    var container = _dataQueueCommand.Take();
                    CommandsPc = container.GetCommand();
                    Notify();


                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                Thread.Sleep(500);

            }
        }
        
         /// <summary>
        /// kalder metoden startMeasure på datalaget
        /// </summary>
         public void RunMeasurement()
        {
            dataControllerObj.StartMeasure();
        }
         /// <summary>
         /// får en liste af målinger over fem sekunder til udregning af nulpunktsjustering. sender listen videre til zeroadjustment, som returnere middelværdien, som bliver sendt til datalaget, hvor det sendes til UI
         /// </summary>
        public void DoZeroAdjusment()
        {
            var zeroAdjustVals = dataControllerObj.StartZeroAdjust();
            zeroAdjustMean = zeroAdjust.CalculateZeroAdjustMean(zeroAdjustVals);
            dataControllerObj.SendZero(zeroAdjustMean);
        }
         /// <summary>
         /// får en liste af målinger over fem sekunder til udregning af Kalibrering. sender listen videre til Calibration, som returnere middelværdien, som bliver sendt til datalaget, hvor det  sendes til UI
         /// </summary>
        public void DoCalibration()
        {
            var calibrationVals = dataControllerObj.StartCal();
            calibrationMean = calibration.CalculateMeanVal(calibrationVals, zeroAdjustMean);
            dataControllerObj.SendMeanCal(calibrationMean);
        }
        /// <summary>
        /// burde mute alarmen. ikke implementeret
        /// </summary>
        public void Mute()
        {
            //Virker ikke
            dataControllerObj.MuteAlarm();
            Thread.Sleep(300000);
        }
        /// <summary>
        /// consumer på limitvals. hiver commands ud af datakøen til commands, så vi kan gå "mod lagenes retning". notifyer samtidig presentationcontrolleren(observeren) om at der ny data
        /// </summary>
        public void RunLimit()  
        {
            while (!_dataQueueLimit.IsCompleted)
            {
                try
                {
                    var container = _dataQueueLimit.Take();
                    LimitVals = container.GetLimitVals();
                    NotifyL();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }

        }
        /// <summary>
        /// consumer på commands. hiver commands ud af datakøen til commands, så vi kan gå "mod lagenes retning".
        /// kalder processing, som skal omregne værdierne fra ADC'en som er i bits til mmHg. Tilføjer blodtryksværdierne til en liste, der bliver sendt med når listen har en vis værdi til calculateBloodpreasure, som udregner værdierne for blodtrykket
        /// </summary>
        public void StartProcessing()  
        {

            int count = 0;

            while (_startMonitoring) 
            {
                while (!_dataQueueMeasure.IsCompleted) 
                {
                    try
                    {
                        var container = _dataQueueMeasure.Take();
                        var rawMeasure = container._buffer;
                        var raw = processing.MakeDtoRaw(rawMeasure, CalibrationValue, zeroAdjustMean);

                        foreach (var measure in raw)
                        {
                            bpList.Add(measure.mmHg);
                            count++;
                        }
                        if (count >= bpList.Capacity)
                        {
                            CalculateBloodPressureVals(bpList);
                            bpList = new List<double>(546);
                            count = 0;
                        }

                        dataControllerObj.SendRaw(raw);

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// starter udregning af blodtryksværdierne. sammenligner de udregnede værdier med grænseværdierne. finder batteristatusen. tilføjer alt dette til en dto, der bliver sendt til til datalaget, som sender videre til UI.
        /// checker desuden eget system for om alarm skal starte
        /// </summary>
        /// <param name="rawMeasure"></param>
        public void CalculateBloodPressureVals(List<double> rawMeasure)
        {
            Bp = processing.CalculateData(rawMeasure);
            limitValExceeded = compare.LimitValExceeded(Bp);
            calculated = new DTO_Calculated(limitValExceeded.HighSys, limitValExceeded.LowSys,
                limitValExceeded.HighDia, limitValExceeded.LowDia, limitValExceeded.HighMean,
                limitValExceeded.LowMean, Bp.CalculatedSys, Bp.CalculatedDia, Bp.CalculatedMean,
                Bp.CalculatedPulse, CheckBattery(), DateTime.UtcNow);

            dataControllerObj.SendDTOCalcualted(calculated);
            CheckLimitVals(limitValExceeded);
        }
        /// <summary>
        /// tjekker batteristatus og returnerer denne. tænder LED'en når batteristatus er under 20%
        /// </summary>
        /// <returns>batteristatus</returns>
        private int CheckBattery()
        {

            var batteryLevel = (batteryStatus.CalculateBatteryStatus(dataControllerObj.GetBatterystatus()));
            if (batteryLevel < 20)
            {
                dataControllerObj.IndicateLowBattery();
                ledOn = true;
            }

            if (ledOn && batteryLevel > 20)
            {
                dataControllerObj.TurnOffLed();
            }

            return batteryLevel;
        }
        /// <summary>
        /// tjekker om limitvals, der vil udløse den audiotive alarm, er blevet overskredet
        /// </summary>
        /// <param name="_limitValExceeded">dto af en masse bools, der fortæller om grænseværdier er overskredet</param>
        public void CheckLimitVals(DTO_ExceededVals _limitValExceeded)
        {
            if (_limitValExceeded.HighSys)
            {
                //Thread highSysThread = new Thread(dataControllerObj.AlarmRequestStart);
                //highSysThread.Start(highSys);
                dataControllerObj.AlarmRequestStart(highSys);
                AlarmOn = true;
                Thread.Sleep(500);
            }

            if (_limitValExceeded.LowMean)
            {
                dataControllerObj.AlarmRequestStart(lowMean);
                //lowMeanThread = new Thread(dataControllerObj.AlarmRequestStart);
                //lowMeanThread.Start(lowMean);
                AlarmOn = true;
                Thread.Sleep(500);
            }

            if (AlarmOn && _limitValExceeded.HighSys == false)
            {
                dataControllerObj.StopAlarm("highSys");
                AlarmOn = false;
            }

            if (AlarmOn && _limitValExceeded.LowMean == false)
            {
                dataControllerObj.StopAlarm("lowMean");
                AlarmOn = false;
            }
        }
       /// <summary>
       /// kalder metoden i limitvals, der sætter limitvals dem, der bliver modtaget fra UI
       /// </summary>
       /// <param name="limitVals">en liste af limitvals</param>
        public void SetLimitVals(DTO_LimitVals limitVals)
        {
            compare.SetLimitVals(limitVals);
        }
        /// <summary>
        /// sætter zeroadjustmean til den værdi, der er kommet fra UI
        /// </summary>
        /// <param name="limitValsZeroVal">nulpunktsværdi fra UI</param>
        public void SetZeroAdjust(in double limitValsZeroVal)
        {
            zeroAdjustMean = limitValsZeroVal;
        }
    }
}
