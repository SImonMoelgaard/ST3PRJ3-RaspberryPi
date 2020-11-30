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
        private string _commandsPc;


        private readonly BlockingCollection<DataContainerUdp> _dataQueueUdpCommand;
        private readonly BlockingCollection<DataContainerMeasureVals> _dataQueueMeasure;
        

        public BusinessController(BlockingCollection<DataContainerUdp> dataQueueUdpCommand, BlockingCollection<DataContainerMeasureVals> dataQueueMeasure)
        {
            _dataQueueUdpCommand = dataQueueUdpCommand;
            _dataQueueMeasure = dataQueueMeasure;

            dataControllerObj= new DataController(_dataQueueMeasure);
        }

        public void RunCommands()
        {
            while (!_dataQueueUdpCommand.IsCompleted)
            {
                try
                {
                    var container = _dataQueueUdpCommand.Take(); 
                    var commandsPc = container.GetCommand();
                    Notify();

                    //switch (commandsPc)
                    //{
                    //    case "Startmeasurment":
                    //    {
                    //        _startMonitoring = true;
                    //        Thread processingThread = new Thread(StartProcessing);
                    //        Thread checkLimitValsThread = new Thread(CalculateBloodpreassureVals);
                    //        processingThread.Start(_startMonitoring);
                    //        checkLimitValsThread.Start();
                    //        break;
                    //    }

                    //    case "Startzeroing": 
                    //    {
                    //        var zeroAdjustVals = dataControllerObj.StartZeroAdjust();
                    //        zeroAdjustMean = zeroAdjust.CalculateZeroAdjustMean(zeroAdjustVals);
                    //        dataControllerObj.SendZero(zeroAdjustMean);
                    //        break;
                    //    }

                    //    case "Startcalibration":
                    //    {
                    //        var calibrationVals = dataControllerObj.StartCal();
                    //        calibrationMean = calibration.CalculateMeanVal(calibrationVals);
                    //        dataControllerObj.SendMeanCal(calibrationMean);
                    //        break;
                    //    }

                    //    case "Mutealarm": 
                    //    {
                    //        dataControllerObj.MuteAlarm();
                    //        break;
                    //    }

                    //    case "Stop":
                    //    {
                    //        _startMonitoring = false;
                    //        StartProcessing(_startMonitoring);
                    //        break;

                    //    }
                            
                   // }


                }
                catch (InvalidOperationException)
                {

                }

                Thread.Sleep(500);
            }
        }

        public string ObserverTest()
        {
            while (!_dataQueueUdpCommand.IsCompleted)
            {
                try
                {
                    var container = _dataQueueUdpCommand.Take();
                    _commandsPc = container.GetCommand();

                }
                catch (InvalidOperationException)
                {

                }
            }

            return _commandsPc;
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
            calibrationMean = calibration.CalculateMeanVal(calibrationVals);
            dataControllerObj.SendMeanCal(calibrationMean);
        }

        public void Mute()
        {
            dataControllerObj.MuteAlarm();
        }

        public void RunLimit() //er det den her, der tester for om der er kommet nye limitvals? for så vil jeg gerne - om muligt have den ind i observeren
        {
            while (!_dataQueueUdpCommand.IsCompleted)
            {
                try
                {
                    var container = _dataQueueUdpCommand.Take();
                    var dtoLimit = container.GetLimitVals();
                    compare.SetLimitVals(dtoLimit);
                    if (dtoLimit.CalVal != null) //der vil altid blive sendt en Kalibrerinsværdi når programmet stater. hvis limitvals ændres undervej i programmet, vil programmet fortsætte med den kalibreringsværdi der blev sendt fra startningen af systemete
                    {
                        calibration.MeanVal = dtoLimit.CalVal;
                    }

                    if (dtoLimit.ZeroVal != null) // denne vil kun ikke være null hvis der bliver trykket på oh shit knappen.
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

        public void CalculateBloodpreassureVals()
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
                            Bp.CalculatedPulse, batteryStatus.CalculateBatteryStatus());

                        dataControllerObj.SendDTOCalcualted(calculated);
                        
                        if (limitValExceeded.HighSys)
                        {
                            dataControllerObj.AlarmRequestStart("highSys");
                            AlarmOn = true;
                        }

                        if (limitValExceeded.LowMean)
                        {
                            dataControllerObj.AlarmRequestStart("lowMean");
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
                    throw;
                }
            }
        }

    }
}
