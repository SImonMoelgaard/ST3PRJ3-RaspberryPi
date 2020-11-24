using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLogic;
using DTO_s;

namespace BusinessLogic
{
    public class BusinessController
    {
        public double CalibrationValue { get; set; }
        private bool AlarmOn { get; set; }
        //public double ZeroAdjustVal { get; set; }
        private DTO_Raw _raw;
        private DTO_BP _bp;
        private DTO_Calculated _calculated;
        private readonly ZeroAdjustment _zeroAdjust= new ZeroAdjustment();
        public DataController DataController = new DataController();
        private readonly Processing _processing = new Processing();
        private readonly Compare _compare = new Compare();
        private readonly Calibration _calibration= new Calibration();
        private readonly BatteryStatus _batteryStatus = new BatteryStatus();
        private double _zeroAdjustMean;
        private double _calibrationMean;
        
        public void DoZeroAdjust(List<double> zeroAdjustVals)
        {
            _zeroAdjustMean= _zeroAdjust.CalculateZeroAdjustMean(zeroAdjustVals);
            DataController.SendZero(_zeroAdjustMean);
        }

        public void OldZeroVal(double zeroVal)
        {
            _zeroAdjust.ZeroAdjustMean = zeroVal;
        }

        public void DoCalibration(List<double> calVals)
        {
            _calibrationMean = _calibration.CalculateMeanVal(calVals);
            DataController.SendMeanCal(_calibrationMean);
        }

        public void StartProcessing(object rawData)
        {
            double _rawData = (double) rawData;
            //det er bl.a. her der skal være tråde
            _raw= _processing.MakeDtoRaw(_rawData, CalibrationValue, _zeroAdjustMean);
            DataController.SendRaw(_raw);
        }

        public void CalculateBloodpreasureVals()
        {
            _bp = _processing.CalculateData();
            var limitValExceeded = _compare.LimitValExceeded(_bp);
            _calculated = new DTO_Calculated(limitValExceeded.HighSys, limitValExceeded.LowSys, limitValExceeded.HighDia , limitValExceeded.LowDia, limitValExceeded.HighMean, limitValExceeded.LowMean, _bp.CalculatedSys, _bp.CalculatedDia, _bp.CalculatedMean, _bp.CalculatedPulse, _batteryStatus.CalculateBatteryStatus());

            DataController.SendDTOCalcualted(_calculated);
            

            if (limitValExceeded.HighSys)
            {
                DataController.AlarmRequestStart("highSys");
                AlarmOn = true;
            }
            if (limitValExceeded.LowMean)
            {
                DataController.AlarmRequestStart("lowMean");
                AlarmOn = true;
            }

            if (AlarmOn && limitValExceeded.HighSys == false)
            {
                DataController.StopAlarm("highSys");
                AlarmOn = false;
            }

            if (AlarmOn && limitValExceeded.LowMean == false)
            {
                DataController.StopAlarm("lowMean");
                AlarmOn = false;
            }
        }


        public void DoLimitVals(DTO_LimitVals limitVals)
        {
            _compare.SetLimitVals(limitVals);
        }

        public void StartMute()
        {
            DataController.MuteAlarm();
        }

       
    }
}
