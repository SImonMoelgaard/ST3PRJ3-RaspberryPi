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
        private readonly ReceiveAdc _adc= new ReceiveAdc();
        private readonly BusinessController _businessController= new BusinessController();

        public void MuteRequest()
        {
            _businessController.StartMute();
        }

        public void ZeroValReceived(double zeroVal)
        {
            _businessController.OldZeroVal(zeroVal);
        }

        public void CalibrationRequest()
        {
            var calibrationVals = _adc.MeasureCalibration();
            _businessController.DoCalibration(calibrationVals);
        }
        public void ZeroAdjustRequest()
        {
            var zeroAdjustVals=_adc.StartZeroAdjust();
            _businessController.DoZeroAdjust(zeroAdjustVals);
        }

        public void LimitValsEntered(DTO_LimitVals limitVals)
        {
            _businessController.DoLimitVals(limitVals);
        }

        public void StartMonitoringRequest()
        {
            
            Thread processingThread= new Thread(_businessController.StartProcessing);



            Thread checkLimitValsThread= new Thread(_businessController.CalculateBloodpreasureVals);
            
            processingThread.Start(_adc.Measure());
            checkLimitValsThread.Start();




           // _businessController.StartProcessing(_adc.Measure());

        }

        public void CalibrationVal(double calibrationVal)
        {
            _businessController.CalibrationValue = calibrationVal;
        }


    public void StopMonitoring()
        {
            //Stop tråden(e), der kører
        }
    }
}
