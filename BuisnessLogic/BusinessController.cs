using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLogic;
using DTO_s;

namespace BusinessLogic
{
    public class BusinessController
    {
        public double calibrationValue;
        private ZeroAdjustment zeroAdjust= new ZeroAdjustment();
        public DataController dataControllerObj = new DataController();
        private Processing processing = new Processing();
        private Compare compare = new Compare();
        private BatteryStatus batteryStatus = new BatteryStatus();
        private double zeroAdjustMean;
        /// <summary>
        /// DTO_calculated objekt, som senere får alle informationerne, der skal sendes videre til UI
        /// </summary>
        private DTO_Calculated CalculatedObj;
        private Calibration calibration= new Calibration();
        private double calibrationMean;
      
        public void DoZeroAdjust(List<double> zeroAdjustVals)
        {
            zeroAdjustMean= zeroAdjust.CalculateZeroAdjustMean(zeroAdjustVals);
            dataControllerObj.SendZero(zeroAdjustMean);
        }

        public void DoCalibration(List<double> calVals)
        {
            calibrationMean = calibration.CalculateMeanVal(calVals);
            dataControllerObj.SendMeanCal(calibrationMean);
        }

        public void StartProcessing(double rawData)
        {
            processing.MakeRaw(rawData, calibrationValue, zeroAdjustMean);
            compare.LimitValExceeded(processing.CalculateData());

            //denne metode kalder (indtil videre) 3 metoder... skal det gøres anderledes(smartere?)
            //også i forhold til at der skal laves selvmetoden med makeDTOCalculated, som skal bruge info herfra?
        }

        public DTO_Calculated MakeDTOCalculated()
        {
            CalculatedObj = new DTO_Calculated(0, 0, 0, 0, batteryStatus.CalculateBatteryStatus(), compare.exceededVals);
            dataControllerObj.SendCalculated(CalculatedObj);
            return CalculatedObj;
        }

    
        public void DoLimitVals(DTO_LimitVals limitVals)
        {
            compare.SetLimitVals(limitVals);
        }

        
    }
}
