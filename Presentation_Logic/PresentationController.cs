using System;
using System.Collections.Concurrent;
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
        private AutoResetEvent _commandReady = new AutoResetEvent(false); // Kig i hospitalssengen. Tror ikke det skal bruges 
        private BusinessController _businessController;
        private string commandsPc;
        private bool _startMonitoring;
        public bool SystemOn { get; private set; }
        private DTO_LimitVals _limitVals;


        public PresentationController(BusinessController businessController )
        {
            _businessController = businessController;
            BusinessController.Attach(this);
            SystemOn = true;
            //commandsPc = "Startmeasurment";  //den her skal ikke være der i det virkelige program
        }

        public void Update()
        {
            commandsPc = _businessController.RunCommands();
            _commandReady.Set();

        }

        public void UpdateLimit()
        {
            //_limitVals = _businessController.RunLimit();
        }

        //public void RunLimit() // Marie... denne skal også skrives som observer :-**** 
        //{
           
        //        while (SystemOn)
        //        {
        //            try
        //            {
        //                _businessController.setLimitVals(_limitVals);
        //                if (_limitVals.CalVal != null)
        //                {
        //                    _businessController.setCalibration(_limitVals.CalVal);
        //                }

        //                if (_limitVals.ZeroVal != null)
        //                {
        //                    _businessController.setZeroAdjust(_limitVals.ZeroVal);
        //                }

        //                //compare.SetLimitVals(dtoLimit);
        //                //if (dtoLimit.CalVal != 0) //der vil altid blive sendt en Kalibrerinsværdi når programmet stater. hvis limitvals ændres undervej i programmet, vil programmet fortsætte med den kalibreringsværdi der blev sendt fra startningen af systemete
        //                //{
        //                //    calibration.MeanVal = dtoLimit.CalVal;
        //                //}

        //                //if (dtoLimit.ZeroVal != 0) // denne vil kun ikke være null hvis der bliver trykket på oh shit knappen.
        //                //{
        //                //    zeroAdjust.ZeroAdjustMean = dtoLimit.ZeroVal;
        //                //}

        //            }
        //            catch (Exception e)
        //            {
        //                Console.WriteLine(e);
        //                throw;
        //            }
        //        }
            
        //}

        public void RunCommands()
        {
            
            while (true/*SystemOn*/) 
            {
                //_commandReady.WaitOne();
                try
                {

                    switch (commandsPc)
                    {
                        case "Startmeasurment":
                        {
                            _startMonitoring = true;

                            Thread processingThread = new Thread(_businessController.StartProcessing);

                            //Thread checkLimitValsThread = new Thread(_businessController.CalculateBloodpreassureVals);
                            processingThread.Start(_startMonitoring);
                            //checkLimitValsThread.Start();
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
                            _businessController.Mute();
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
                            //tænker den skal have en metode i logic, der kalder videre til data
                            break;
                        }

                    }


                }
                catch (InvalidOperationException)
                {
                    
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
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