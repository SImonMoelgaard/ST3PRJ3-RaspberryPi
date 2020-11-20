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
        private double zeroAdjustMean;
        public void DoZeroAdjust(List<double> zeroAdjustVals)
        {
            zeroAdjustMean= zeroAdjust.CalculateZeroAdjustMean(zeroAdjustVals);
            dataControllerObj.SendZero(zeroAdjustMean);
        }

        public void DoCalibration(List<double> calVals)
        {
            var calibrationMean = 0;
        }

        public void StartProcessing(double rawData)
        {
            processing.ConvertBp(rawData, calibrationValue, zeroAdjustMean);
        }

        public void DoLimitVals(DTO_LimitVals limitVals)
        {
            compare.SetLimitVals(limitVals);
        }
    }
}
