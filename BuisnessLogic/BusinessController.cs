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
        //public double ZeroAdjustVal { get; set; }
        private DTO_Raw raw;
        private DTO_Bloodpreassure Bp;
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
            Bp = processing.CalculateData(raw);
            compare.LimitValExceeded(Bp);
            dataControllerObj.SendDTOCalcualted(MakeDTOCalculated());
        }

        public void CheckLimitVals()
        {

        }

        private DTO_Calculated MakeDTOCalculated()
        {
            calculated = new DTO_Calculated(Bp.CalculatedSys,Bp.CalculatedDia, Bp.CalculatedMean, Bp.CalculatedPulse, batteryStatus.CalculateBatteryStatus());
            return calculated;
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
