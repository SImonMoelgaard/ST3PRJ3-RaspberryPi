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
       private AutoResetEvent _commandReady = new AutoResetEvent(false); 
       private AutoResetEvent _limitReady = new AutoResetEvent(false);
        private BusinessController _businessController;
        private string commandsPc;
        private bool _startMonitoring;
       
        private DTO_LimitVals _limitVals;


        public PresentationController(BusinessController businessController )
        {
            _businessController = businessController;
            BusinessController.Attach(this);
            _businessController.SetSystemOn(true);
        }

        public void Update()
        {
            commandsPc = _businessController.CommandsPc;
           _commandReady.Set();
            CheckCommands();
        }

        public void UpdateLimit()
        {
            _limitVals = _businessController.LimitVals;
            //_businessController.CalibrationValue = _limitVals.CalVal;
            // _businessController.Z
            
            _limitReady.Set();
            CheckLimit();

        }

        public void RunProducerCommands() //TRÅD!
        {
            _businessController.StartProducerCommands(); 
        }

        public void RunProducerLimit() //TRÅD! 
        {
            _businessController.StartProducerLimit();
        }

        public void RunConsumerLimit()
        {
            _businessController.RunLimit();
        }

        public void RunConsumerCommands()
        {
            _businessController.RunCommands();
            
        }

        public void CheckLimit() 
        {

            while (_businessController.GetSystemOn())
            {
                _limitReady.WaitOne();
                try
                {
                    _businessController.setLimitVals(_limitVals);
                    if (_limitVals.CalVal != 0)
                    {
                        _businessController.setCalibration(_limitVals.CalVal);
                    }

                    if (_limitVals.ZeroVal != 0)
                    {
                        _businessController.setZeroAdjust(_limitVals.ZeroVal);
                    }
                    
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }

        }


        public void CheckCommands()
        {

            while (_businessController.GetSystemOn()) 
            {

                _commandReady.WaitOne();
                try
                {
                    switch (commandsPc)
                    {
                        case "Startmeasurment":
                            {
                                _businessController.StartMonitoring = true;
                                Thread measurementThread = new Thread(_businessController.RunMeasurement);
                                Thread processingThread = new Thread(_businessController.NewStartProcessing);
                                // Thread calculateBloodpreassureThread = new Thread(_businessController.NewCalculateBloodPressureVals);
                                measurementThread.Start();
                                processingThread.Start(); //exception her out of memory
                                //calculateBloodpreassureThread.Start();
                                break;
                            }

                        case "Startzeroing":
                            {
                                _businessController.DoZeroAdjusment();
                                break;
                            }

                        case "Startcalibration":
                            {
                                _businessController.DoCalibration();
                                break;
                            }

                        case "Mutealarm":
                            {
                                _businessController.Mute();
                                break;
                            }

                        case "Stop":
                            {
                                _businessController.StartMonitoring = false;
                                break;

                            }
                        case "SystemOff":
                            {
                                _businessController.SetSystemOn(false);
                                break;
                            }

                    }


                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                Thread.Sleep(500);
            }


        }


      
    }
}