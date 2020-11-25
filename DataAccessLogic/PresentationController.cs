using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using BusinessLogic;
using DTO_s;


namespace PresentationLogic
{
    public class PresentationController
    {
        private ReceiveAdc adc= new ReceiveAdc();
        private BusinessController logicObj= new BusinessController();

        public void MuteRequest()
        {
            logicObj.StartMute();
        }

        public void ZeroValReceived(double zeroVal)
        {
            logicObj.OldZeroVal(zeroVal);
        }

        public void CalibrationRequest()
        {
            var calibrationVals = adc.MeasureCalibration();
            logicObj.DoCalibration(calibrationVals);
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
            
            Thread processingThread= new Thread(logicObj.StartProcessing);



            Thread checkLimitValsThread= new Thread(logicObj.CheckLimitVals);
            
            processingThread.Start(adc.Measure());
            checkLimitValsThread.Start();




           // logicObj.StartProcessing(adc.Measure());

        }

        public void CalibrationVal(double calibrationVal)
        {
            logicObj.CalibrationValue = calibrationVal;
        }


    public void StopMonitoring()
        {
            //Stop tråden(e), der kører
        }
    }
}
