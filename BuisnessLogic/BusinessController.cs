using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLogic;
using DTO_s;

namespace BusinessLogic
{
    public class BusinessController
    {
        private ZeroAdjustment zeroAdjust= new ZeroAdjustment();
        public DataController dataControllerObj = new DataController();
        public void DoZeroAdjust(List<double> zeroAdjustVals)
        {
            var zeroAdjustMean= zeroAdjust.CalculateZeroAdjustMean(zeroAdjustVals);
            dataControllerObj.SendZero(zeroAdjustMean);
        }

        public void DoCalibration(List<double> calVals)
        {
            var calibrationMean = 0;
        }

        public void StartProcessing(double measure)
        {
            
        }

        public void DoLimitVals(DTO_LimitVals limitVals)
        {
            throw new NotImplementedException();
        }
    }
}
