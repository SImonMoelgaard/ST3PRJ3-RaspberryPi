using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using BusinessLogic;
using DataAccessLogic;
using DTO_s;


namespace BP_program
{
    public class PresentationController : IPresentationObserver
    {
        private AutoResetEvent _commandReady = new AutoResetEvent(false);
        private BusinessController _businessController;
        private string commandsPc;
        private bool _startMonitoring;

        public PresentationController(BusinessController businessController)
        {
            _businessController = businessController;
            BusinessController.Attach(this);
        }

        //public PresentationController()
        //{
        //}

        public void Update()
        {
            commandsPc = _businessController.ObserverTest();
            _commandReady.Set();
        }

        public void RunCommands()
        {
            while (true) //skal måske ikke være en evig løkke
            {
                _commandReady.WaitOne();
                try
                {
                    switch (commandsPc)
                    {
                        case "Startmeasurment":
                        {
                            _startMonitoring = true;
                            Thread processingThread = new Thread(_businessController.StartProcessing);
                            Thread checkLimitValsThread = new Thread(_businessController.CalculateBloodpreassureVals);
                            processingThread.Start(_startMonitoring);
                            checkLimitValsThread.Start();
                            break;
                        }

                        case "Startzeroing":
                        {
                            _businessController.ZeroAdjusment();
                            break;
                        }

                        case "Startcalibration":
                        {
                            _businessController.Calibration();
                            break;
                        }

                        case "Mutealarm":
                        {

                            break;
                        }

                        case "Stop":
                        {
                            _startMonitoring = false;
                            _businessController.StartProcessing(_startMonitoring);
                            break;

                        }
                        case "SystemOff":
                        {
                            //Her sættes SystemOff bool til false, men jeg ved ikke líge hvordan jeg skal få fat i den, når den ligger i datalaget.. 
                            break;
                        }

                    }


                }
                catch (InvalidOperationException)
                {

                }

                Thread.Sleep(500);
            }







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

        }

    }
}