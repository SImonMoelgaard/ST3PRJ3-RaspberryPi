using System;
using System.Collections.Generic;
using System.Text;
using BusinessLogic;
using DTO_s;


namespace PresentationLogic
{
    class PresentationController
    {
        private ReceiveAdc adc= new ReceiveAdc();
        private BusinessController logicObj= new BusinessController();
        

        public void CalibrationRequest()
        {
            var calibrationVal = adc.MeasureCalibration();
            logicObj.DoCalibration(calibrationVal);
        }
        public void ZeroAdjustRequest()
        {
            var zeroAdjustVals=adc.StartZeroAdjust();
            logicObj.DoZeroAdjust(zeroAdjustVals);
        }

        public void LimitValsEntered(DTO_LimitVals limitVals)
        {
            logicObj.DoLimitVals(limitVals);
        }

        public void StartMonitoringRequest()
        {
            
            logicObj.StartProcessing(adc.Measure());

        }

        public void CalibrationVal(double calibrationVal)
        {
            logicObj.calibrationValue = calibrationVal;
        }

        public void StopMonitoring()
        {
            //Stop tråden(e), der kører
        }
    }
}
