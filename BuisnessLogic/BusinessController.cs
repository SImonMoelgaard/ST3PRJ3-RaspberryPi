﻿using System;
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
        private DTO_Raw raw;
        private DTO_Calculated calculated;
        private DTO_exceedVals exceedVals;
        private ZeroAdjustment zeroAdjust= new ZeroAdjustment();
        public DataController dataControllerObj = new DataController();
        private Processing processing = new Processing();
        private Compare compare = new Compare();
        private Calibration calibration= new Calibration();
        private BatteryStatus batteryStatus = new BatteryStatus();
        private double zeroAdjustMean;
        private double calibrationMean;
        
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

        public void StartProcessing(double rawData)
        {
            //det er bl.a. her der skal være tråde
            raw= processing.MakeDTORaw(rawData, CalibrationValue, zeroAdjustMean);
            dataControllerObj.SendRaw(raw);
            calculated = processing.CalculateData(raw);
            CheckLimitVals(calculated);
            
            dataControllerObj.SendDTOCalcualted(calculated);
        }

        public void CheckLimitVals(DTO_Calculated bp)
        {
            var limitValExceeded = compare.LimitValExceeded(calculated);
            dataControllerObj.SendExceededVals(limitValExceeded);

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

       
    }
}
