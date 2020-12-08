using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using DataAccessLogic;
using DTO_s;

namespace BusinessLogic
{
    public class BusinessController : UdpProvider
    {
        public double CalibrationValue { get; set; }
        /// <summary>
        /// Liste bestående af 546, svarende til det antal målinger der sker på 3 sekunder.
        /// </summary>
        private readonly List<double> _bpList = new List<double>(546);
        ///// <summary>
        ///// Liste bestående af 45 målinger, ca svarende til målinger over 1/4 sekund
        ///// </summary>
        private readonly List<DTO_Raw> _rawList = new List<DTO_Raw>(45);
        private bool AlarmOn { get; set; }
        //public double ZeroAdjustVal { get; set; }
        //private DTO_Raw raw;
        private bool _startMonitoring;

      
        private DTO_BP Bp;
        private DTO_Calculated calculated;
        private DTO_ExceededVals exceededVals;
        private ZeroAdjustment zeroAdjust= new ZeroAdjustment();
        public DataController dataControllerObj;
        private Processing processing = new Processing();
        private Compare compare = new Compare();
        private Calibration calibration= new Calibration();
        private BatteryStatus batteryStatus = new BatteryStatus();
        private double zeroAdjustMean;
        private double calibrationMean;
        private bool _systemOn;
       
        private string highSys;
        private string lowMean;
        private Thread lowMeanThread;
        private Thread highSysThread;


        private readonly BlockingCollection<DataContainerUdp> _dataQueueCommand;
        private readonly BlockingCollection<DataContainerMeasureVals> _dataQueueMeasure;
        private readonly BlockingCollection<DataContainerUdp> _dataQueueLimit;
        

        public BusinessController(BlockingCollection<DataContainerUdp> dataQueueCommand,BlockingCollection<DataContainerUdp> dataQueueLimit ,BlockingCollection<DataContainerMeasureVals> dataQueueMeasure )
        {
            _dataQueueCommand = dataQueueCommand;
            _dataQueueMeasure = dataQueueMeasure;
            _dataQueueCommand = dataQueueCommand;

            dataControllerObj= new DataController(_dataQueueMeasure, _dataQueueLimit, _dataQueueCommand);
        }

        public void StartProducerLimit() //Tråd på denne 
        {
            dataControllerObj.ProducerLimitRun();
        }

        public void StartProducerCommands()
        {
            dataControllerObj.ProducerCommandsRun();
        }

        public void SetSystemOn(bool systemOn)
        {
            _systemOn = systemOn;
            dataControllerObj.ReceiveSystemOn(_systemOn);
        }

        public bool GetSystemOn() 
        {
            return _systemOn;
        }
        public string RunCommands() //Consumer på commands 
        {
            while (!_dataQueueCommand.IsCompleted)
            {
                try
                {
                    var container = _dataQueueCommand.Take(); 
                    var _commandsPc = container.GetCommand();
                    Notify();
                    return _commandsPc;



                }
                catch (Exception e)
                {
                   Console.WriteLine(e);
                }

                Thread.Sleep(500);
               
            }

            return null;
        }

   

        public void ZeroAdjusment()
        {
            var zeroAdjustVals = dataControllerObj.StartZeroAdjust();
            zeroAdjustMean = zeroAdjust.CalculateZeroAdjustMean(zeroAdjustVals);
            dataControllerObj.SendZero(zeroAdjustMean);
        }

        public void Calibration()
        {
            var calibrationVals = dataControllerObj.StartCal();
            calibrationMean = calibration.CalculateMeanVal(calibrationVals, zeroAdjustMean);
            dataControllerObj.SendMeanCal(calibrationMean);
        }

        public void Mute()
        {
            //ikke sikker på det her virker
            dataControllerObj.MuteAlarm();
            Thread.Sleep(300000);
        }

        public void RunLimit() // consumer på limitvals 
        {
            while (!_dataQueueCommand.IsCompleted)
            {
                try
                {
                    var container = _dataQueueCommand.Take();
                    var dtoLimit = container.GetLimitVals();
                    compare.SetLimitVals(dtoLimit);
                    if (dtoLimit.CalVal != 0) //der vil altid blive sendt en Kalibrerinsværdi når programmet stater. hvis limitvals ændres undervej i programmet, vil programmet fortsætte med den kalibreringsværdi der blev sendt fra startningen af systemete
                    {
                        calibration.MeanVal = dtoLimit.CalVal;
                    }

                    if (dtoLimit.ZeroVal != 0) // denne vil kun ikke være null hvis der bliver trykket på oh shit knappen.
                    {
                        zeroAdjust.ZeroAdjustMean = dtoLimit.ZeroVal;
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        public void StartProcessing(object startMonitoring)
        {
            bool _startMonitoring = (bool) startMonitoring;

            while (_startMonitoring)
            {
                int count = 0;
                while (count != _rawList.Capacity)
                {
                    var _measureVal = dataControllerObj.StartMeasure();
                    var raw = processing.MakeDtoRaw(_measureVal, CalibrationValue, zeroAdjustMean);
                    _rawList.Add(raw);
                    count++;
                }
                dataControllerObj.SendRaw(_rawList);
            }
        }

        public void CalculateBloodpreassureVals() //Consumer på Measure 
        {
            int count = 0;
            while (!_dataQueueMeasure.IsCompleted)
            {
                try
                {
                    var container = _dataQueueMeasure.Take();
                    var rawMeasure = container.GetMeasureVal();
                    _bpList.Add(rawMeasure);
                    count++;
                    if (count == _bpList.Capacity)
                    {
                        Bp = processing.CalculateData(_bpList);
                        var limitValExceeded = compare.LimitValExceeded(Bp);
                        calculated = new DTO_Calculated(limitValExceeded.HighSys, limitValExceeded.LowSys,
                            limitValExceeded.HighDia, limitValExceeded.LowDia, limitValExceeded.HighMean,
                            limitValExceeded.LowMean, Bp.CalculatedSys, Bp.CalculatedDia, Bp.CalculatedMean,
                            Bp.CalculatedPulse, batteryStatus.CalculateBatteryStatus(dataControllerObj.GetBatterystatus()));

                        dataControllerObj.SendDTOCalcualted(calculated);
                        
                        if (limitValExceeded.HighSys)
                        {
                            Thread highSysThread = new Thread(dataControllerObj.AlarmRequestStart);
                            highSysThread.Start(highSys);
                            //dataControllerObj.AlarmRequestStart("highSys");
                            AlarmOn = true;
                        }

                        if (limitValExceeded.LowMean)
                        {
                            lowMeanThread = new Thread(dataControllerObj.AlarmRequestStart);
                            lowMeanThread.Start(lowMean);
                            AlarmOn = true;
                        }

                        if (AlarmOn && limitValExceeded.HighSys == false)
                        {
                            dataControllerObj.StopAlarm("highSys");
                            AlarmOn = false;
                        }

                        if (AlarmOn && limitValExceeded.LowMean == false)
                        {
                            dataControllerObj.StopAlarm("lowMean");
                            AlarmOn = false;
                        }

                        _bpList.Clear();
                        count = 0;

                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        public void setLimitVals(DTO_LimitVals limitVals)
        {
            throw new NotImplementedException();
        }

        public void setCalibration(in double limitValsCalVal)
        {
            throw new NotImplementedException();
        }

        public void setZeroAdjust(in double limitValsZeroVal)
        {
            throw new NotImplementedException();
        }

        public void SetLimitVals(object dtoLimit)
        {
            throw new NotImplementedException();
        }
    }
}
