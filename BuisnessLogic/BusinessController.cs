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
    public class BusinessController : UdpProvider
    {
        public double CalibrationValue { get; set; }
        /// <summary>
        /// Liste bestående af 546, svarende til det antal målinger der sker på 3 sekunder.
        /// </summary>
        private List<double> _bpList = new List<double>(546);
        ///// <summary>
        ///// Liste bestående af 45 målinger, ca svarende til målinger over 1/4 sekund
        ///// </summary>
        private readonly List<DTO_Raw> _rawList = new List<DTO_Raw>(45);
        private bool AlarmOn { get; set; }
        //public double ZeroAdjustVal { get; set; }
        //private DTO_Raw raw;
        public bool StartMonitoring { get; set; }

      
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
        private DTO_ExceededVals limitValExceeded;
        public string CommandsPc { get; private set; }
        public DTO_LimitVals LimitVals { get; private set; }
        private bool ledOn;
        private string highSys;
        private string lowMean;
        private Thread lowMeanThread;
        private Thread highSysThread;
        private List<double> bpList = new List<double>(546);


        private readonly BlockingCollection<DataContainerUdp> _dataQueueCommand;
        private readonly BlockingCollection<DataContainerMeasureVals> _dataQueueMeasure;
        private readonly BlockingCollection<DataContainerUdp> _dataQueueLimit;
        

        public BusinessController(BlockingCollection<DataContainerUdp> dataQueueCommand,BlockingCollection<DataContainerUdp> dataQueueLimit ,BlockingCollection<DataContainerMeasureVals> dataQueueMeasure )
        {
            _dataQueueCommand = dataQueueCommand;
            _dataQueueMeasure = dataQueueMeasure;
            _dataQueueLimit = dataQueueLimit;
            ledOn = false;
            dataControllerObj= new DataController(_dataQueueMeasure, _dataQueueLimit, _dataQueueCommand);
        }

        public void StartProducerLimit() 
        {
            dataControllerObj.ProducerLimitRun(); //exeption her
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
        public void RunCommands() //Consumer på commands 
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

        public void RunMeasurement()
        {
            dataControllerObj.StartMeasure();
        }

   

        public void DoZeroAdjusment()
        {
            var zeroAdjustList = dataControllerObj.StartZeroAdjust();
            zeroAdjustMean = zeroAdjust.CalculateZeroAdjustMean(zeroAdjustList);
            dataControllerObj.SendZero(zeroAdjustMean);

            //dataControllerObj.NewStartZeroAdjust();
            //var container = _dataQueueMeasure.Take();
            //var zeroAdjustList = container._buffer;

            ////foreach (var VARIABLE in zeroAdjustList)
            ////{

            ////}

            //zeroAdjustMean = zeroAdjust.CalculateZeroAdjustMean(zeroAdjustList);
            //dataControllerObj.SendZero(zeroAdjustMean);
        }

        public void DoCalibration()
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

        //public void StartProcessing(object startMonitoring) //skal skrives om efter rettelser
        //{
        //    bool _startMonitoring = (bool) startMonitoring;

        //    while (_startMonitoring)
        //    {
        //        //int count = 0;
        //        //while (count != _rawList.Capacity)
        //        //{
        //            var _measureVal = dataControllerObj.StartMeasure();
        //            var raw = processing.MakeDtoRaw(_measureVal, CalibrationValue, zeroAdjustMean);
        //            //_rawList.Add(raw);
        //          //  count++;
        //        //}
        //        //dataControllerObj.SendRaw(raw);
        //    }
        //}
        public void NewStartProcessing() // Ny consumer på measure 
        {
           
            int count = 0;
            
            while (StartMonitoring) // Denne burde egentligt ikke være nødvendig længere tror jeg.
            {
                while(!_dataQueueMeasure.IsCompleted) //Tror den bool der kommer til dataQueueMeasure skal være startMonitoring og ikke systemOn
                { 
                    try
                    {
                        var container = _dataQueueMeasure.Take();
                        var rawMeasure = container._buffer;
                        var raw = processing.NewMakeDtoRaw(rawMeasure, CalibrationValue, zeroAdjustMean);
                        
                        foreach (var measure in raw)
                        {
                            bpList.Add(measure.mmHg);
                            count++;
                        }
                        if(count>=bpList.Capacity) 
                        {
                            NewCalculateBloodPressureVals(bpList);
                            bpList = new List<double>(546);
                            //bpList.Clear();
                            count = 0;
                        }
                        
                        dataControllerObj.SendRaw(raw);
                        //Thread.Sleep(20);//skal muligvis kun være her når vi kører med fake
                        
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }
            }
        }
        public void NewCalculateBloodPressureVals(object _rawMeasure)
        {
            List<double> rawMeasure = (List<double>)_rawMeasure;
            Bp = processing.CalculateData(rawMeasure);
            limitValExceeded = compare.LimitValExceeded(Bp);
            calculated = new DTO_Calculated(limitValExceeded.HighSys, limitValExceeded.LowSys,
                limitValExceeded.HighDia, limitValExceeded.LowDia, limitValExceeded.HighMean,
                limitValExceeded.LowMean, Bp.CalculatedSys, Bp.CalculatedDia, Bp.CalculatedMean,
                Bp.CalculatedPulse, CheckBattery(), DateTime.UtcNow);

            dataControllerObj.SendDTOCalcualted(calculated); 
            CheckLimitVals(limitValExceeded);
        }

        //public void CalculateBloodpreassureVals() //Consumer på Measure //skal skrives om efter rettelser
        //{
        //    int count = 0;
        //    while (!_dataQueueMeasure.IsCompleted)
        //    {
        //        try
        //        {
        //            var container = _dataQueueMeasure.Take();
        //            var rawMeasure = container.GetMeasureVal();
        //            _bpList.Add(rawMeasure);
        //            count++;
        //            if (count == _bpList.Capacity)
        //            {
        //                Bp = processing.CalculateData(_bpList);
        //                limitValExceeded = compare.LimitValExceeded(Bp);
        //                calculated = new DTO_Calculated(limitValExceeded.HighSys, limitValExceeded.LowSys,
        //                    limitValExceeded.HighDia, limitValExceeded.LowDia, limitValExceeded.HighMean,
        //                    limitValExceeded.LowMean, Bp.CalculatedSys, Bp.CalculatedDia, Bp.CalculatedMean,
        //                    Bp.CalculatedPulse, CheckBattery()
        //                    ,DateTime.UtcNow); 

        //                dataControllerObj.SendDTOCalcualted(calculated); //pt kommer vi ikke hertil, der bliver aldrig sendt calculated
        //                CheckLimitVals();
        //                _bpList.Clear();
        //                count = 0;
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            Console.WriteLine(e);
        //            throw;
        //        }
        //    }
        //}

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

        public void CheckLimitVals(DTO_ExceededVals _limitValExceeded)
        {
            if (_limitValExceeded.HighSys)
            {
                //Thread highSysThread = new Thread(dataControllerObj.AlarmRequestStart);
                //highSysThread.Start(highSys);
                dataControllerObj.AlarmRequestStart(highSys);
                AlarmOn = true;
            }

            if (_limitValExceeded.LowMean)
            {
                dataControllerObj.AlarmRequestStart(lowMean);
                //lowMeanThread = new Thread(dataControllerObj.AlarmRequestStart);
                //lowMeanThread.Start(lowMean);
                AlarmOn = true;
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
        public void setLimitVals(DTO_LimitVals limitVals)
        {
            compare.SetLimitVals(limitVals);
        }

        public void setCalibration(in double limitValsCalVal)
        {
            CalibrationValue = limitValsCalVal;
        }

        public void setZeroAdjust(in double limitValsZeroVal) 
        {
            zeroAdjustMean = limitValsZeroVal;
        }
    }
}
