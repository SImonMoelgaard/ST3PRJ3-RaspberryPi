using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using DataAccessLogic;
using DTO_s;

namespace BusinessLogic
{
    public class BusinessController
    {
        public double CalibrationValue { get; set; }
        private bool AlarmOn { get; set; }
        //public double ZeroAdjustVal { get; set; }
        //private DTO_Raw raw;

        private DataController dataController;
        private DTO_BP Bp;
        private DTO_Calculated calculated;
        private DTO_ExceededVals exceededVals;
        private ZeroAdjustment zeroAdjust= new ZeroAdjustment();
        public DataController dataControllerObj = new DataController();
        private Processing processing = new Processing();
        private Compare compare = new Compare();
        private Calibration calibration= new Calibration();
        private BatteryStatus batteryStatus = new BatteryStatus();
        private double zeroAdjustMean;
        private double calibrationMean;


        private readonly BlockingCollection<DataContainerUdp> dataQueue;
        

        public BusinessController(BlockingCollection<DataContainerUdp> dataQueue)
        {
            this.dataQueue = dataQueue;
            dataController= new DataController();
        }

        public void Run()
        {
            while (!dataQueue.IsCompleted)
            {
                try
                {
                    var container = dataQueue.Take(); // Tager et objekt ud af min kø når der er noget, ellers venter denn
                    var commandsPc = container.GetCommand();

                    switch (commandsPc)
                    {
                        case "Startmeasurment":
                            Thread processingThread = new Thread(StartProcessing);
                            Thread checkLimitValsThread = new Thread(CalculateBloodpreassureVals);
                            processingThread.Start(adc);
                            checkLimitValsThread.Start();
                            break;

                        case "Startzeroing": // burde være rigtig og fungere nu
                            var zeroAdjustVals = dataController.StartZeroAdjust();
                            DoZeroAdjust(zeroAdjustVals);
                            break;

                        case "Startcalibration": // burde være rigtig og fungere nu
                            var calibrationVals= dataController.StartCal();
                            DoCalibration(calibrationVals);
                            break;

                        case "Mutealarm":
                            StartMute();
                            break;

                        case "Stop":
                            StopMonitoring();
                            break;
                    }


                }
                catch (InvalidOperationException)
                {

                }
                Thread.Sleep(500);
            }
        }



        public void DoZeroAdjust(List<double> zeroAdjustVals)
        {
            zeroAdjustMean= zeroAdjust.CalculateZeroAdjustMean(zeroAdjustVals);
            dataControllerObj.SendZero(zeroAdjustMean);
        }

        public void OldZeroVal(double zeroVal)
        {
            zeroAdjust.ZeroAdjustMean = zeroVal;
        }

        public void DoCalibration(List<double> calVals)
        {
            calibrationMean = calibration.CalculateMeanVal(calVals);
            dataControllerObj.SendMeanCal(calibrationMean);
        }

        public void StartProcessing(object adc)
        {
            ReceiveAdc _adc = (ReceiveAdc) adc;


            double _rawData = _adc.Measure();
            //det er bl.a. her der skal være tråde
            var raw= processing.MakeDTORaw(_rawData, CalibrationValue, zeroAdjustMean);
            dataControllerObj.SendRaw(raw);
            Bc.Add(_rawData);
        }

        public void CalculateBloodpreassureVals()
        {
            var raw=Bc.Take();
            Bp = processing.CalculateData(raw);
            var limitValExceeded = compare.LimitValExceeded(Bp);
            calculated = new DTO_Calculated(limitValExceeded.HighSys, limitValExceeded.LowSys, limitValExceeded.HighDia , limitValExceeded.LowDia, limitValExceeded.HighMean, limitValExceeded.LowMean, Bp.CalculatedSys, Bp.CalculatedDia, Bp.CalculatedMean, Bp.CalculatedPulse, batteryStatus.CalculateBatteryStatus());

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
        }


        public void DoLimitVals(DTO_LimitVals limitVals)
        {
            compare.SetLimitVals(limitVals);
        }

        public void StartMute()
        {
            dataControllerObj.MuteAlarm();
        }

        public void StopMonitoring()
        {

        }

       
    }
}
