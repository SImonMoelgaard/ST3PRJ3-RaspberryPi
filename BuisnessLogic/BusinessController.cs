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
        //private DTO_Raw raw;
        private DTO_BP _bp;
        private DTO_Calculated _calculated;
        private DTO_ExceededVals _exceededVals;
        private ZeroAdjustment zeroAdjust= new ZeroAdjustment();
        private DataController _dataControllerObj = new DataController();
        private Processing processing = new Processing();
        private Compare compare = new Compare();
        private Calibration calibration= new Calibration();
        private BatteryStatus batteryStatus = new BatteryStatus();
        private double zeroAdjustMean;
        private double calibrationMean;
        
        public void DoZeroAdjust(List<double> zeroAdjustVals)
        {
            zeroAdjustMean= zeroAdjust.CalculateZeroAdjustMean(zeroAdjustVals);
            _dataControllerObj.SendZero(zeroAdjustMean);
        }

        public void OldZeroVal(double zeroVal)
        {
            zeroAdjust.ZeroAdjustMean = zeroVal;
        }

        public void DoCalibration(List<double> calVals)
        {
            calibrationMean = calibration.CalculateMeanVal(calVals);
            _dataControllerObj.SendMeanCal(calibrationMean);
        }

        public void StartProcessing(object adc)
        {
            ReceiveAdc _adc = (ReceiveAdc) adc;


            double _rawData = _adc.Measure();
            //det er bl.a. her der skal være tråde
            var raw= processing.MakeDTORaw(_rawData, CalibrationValue, zeroAdjustMean);
            _dataControllerObj.SendRaw(raw);
            Bc.Add(_rawData);
        }

        public void CalculateBloodpreassureVals()
        {
            var raw=Bc.Take();
            _bp = processing.CalculateData(raw);
            _exceededVals = compare.LimitValExceeded(_bp);
            _calculated = new DTO_Calculated(_exceededVals.HighSys, _exceededVals.LowSys, _exceededVals.HighDia , _exceededVals.LowDia, _exceededVals.HighMean, _exceededVals.LowMean, _bp.CalculatedSys, _bp.CalculatedDia, _bp.CalculatedMean, _bp.CalculatedPulse, batteryStatus.CalculateBatteryStatus());

            _dataControllerObj.SendDTOCalcualted(_calculated);
            

            if (_exceededVals.HighSys)
            {
                _dataControllerObj.AlarmRequestStart("highSys");
                AlarmOn = true;
            }
            if (_exceededVals.LowMean)
            {
                _dataControllerObj.AlarmRequestStart("lowMean");
                AlarmOn = true;
            }

            if (AlarmOn && _exceededVals.HighSys == false)
            {
                _dataControllerObj.StopAlarm("highSys");
                AlarmOn = false;
            }

            if (AlarmOn && _exceededVals.LowMean == false)
            {
                _dataControllerObj.StopAlarm("lowMean");
                AlarmOn = false;
            }
        }


        public void DoLimitVals(DTO_LimitVals limitVals)
        {
            compare.SetLimitVals(limitVals);
        }

        public void StartMute()
        {
            _dataControllerObj.MuteAlarm();
        }

       
    }
}
