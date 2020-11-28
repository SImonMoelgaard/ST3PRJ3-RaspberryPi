using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using BusinessLogic;
using DTO_s;


namespace PresentationLogic
{
    public class PresentationController : IPresentationObserver
    {

        //public void ZeroValReceived(double zeroVal)
        //{
        //    _businessController.OldZeroVal(zeroVal);
        //}



        //public void LimitValsEntered(DTO_LimitVals limitVals)
        //{
        //    _businessController.DoLimitVals(limitVals);
        //}



        //public void CalibrationVal(double calibrationVal)
        //{
        //    _businessController.CalibrationValue = calibrationVal;
        //}

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
