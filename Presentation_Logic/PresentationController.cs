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
        }

        public void UpdateLimit()
        {
            _limitVals = _businessController.LimitVals;
            _limitReady.Set();
        }

        public void RunLimit() 
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


        public void RunCommands()
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
                                _startMonitoring = true;

                                Thread processingThread = new Thread(_businessController.StartProcessing);

                                Thread calculateBloodpreassureThread = new Thread(_businessController.CalculateBloodpreassureVals);
                                processingThread.Start(_startMonitoring);
                                calculateBloodpreassureThread.Start();
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