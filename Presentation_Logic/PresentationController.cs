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
    /// <summary>
    /// presentationcontrolleren. Herfra sker kommunikationen videre til businesscontrolleren. er observer på grænseværdier og kommandoer fra PC'en
    /// </summary>
    public class PresentationController : IPresentationObserver
    {
        private AutoResetEvent _commandReady = new AutoResetEvent(false);
        private AutoResetEvent _limitReady = new AutoResetEvent(false);
        private BusinessController _businessController;
        private string commandsPc;
        private DTO_LimitVals _limitVals;

        /// <summary>
        /// constructor for presentationcontroller
        /// </summary>
        /// <param name="businessController">opretter businesscontrolleren</param>
        public PresentationController(BusinessController businessController)
        {
            _businessController = businessController;
            BusinessController.Attach(this);
            _businessController.SetSystemOn(true);
        }
        /// <summary>
        /// bliver kaldt, når der bliver registret en ny kommando fra UI. kalder CheckCommands
        /// </summary>
        public void Update()
        {
            commandsPc = _businessController.CommandsPc;
            _commandReady.Set();
            CheckCommands();
        }
        /// <summary>
        /// bliver kaldt, når der bliver registret en nye grænseværdier fra UI. kalder CheckLimit
        /// </summary>
        public void UpdateLimit()
        {
            _limitVals = _businessController.LimitVals;
            _limitReady.Set();
            CheckLimit();

        }
        /// <summary>
        /// tråd fra programmet kalder den her metode, der starter lytningen efter kommandoer fra UI 
        /// </summary>
        public void RunProducerCommands()
        {
            _businessController.StartProducerCommands();
        }
        /// <summary>
        /// tråd fra programmet kalder den her metode, der starter lytningen efter kommandoer fra UI 
        /// </summary>
        public void RunProducerLimit() 
        {
            _businessController.StartProducerLimit();
        }
        /// <summary>
        /// tråd fra programmet kalder den her metode, der runingen på grænseværdier, så når der er modtaget en ny grænseværdi DTO, fra UI er businesscontrolleren klar til at sende videre
        /// </summary>
        public void RunConsumerLimit()
        {
            _businessController.RunLimit();
        }
        /// <summary>
        /// tråd fra programmet kalder den her metode, der runingen på kommandoer, så når der er modtaget en ny komando fra UI er businesscontrolleren klar til at sende videre
        /// </summary>
        public void RunConsumerCommands()
        {
            _businessController.RunCommands();
        }
        /// <summary>
        /// når der er kommet nye grænseværdier, bliver denne metode kaldt og herfra vil grænseværdierne samt nulpunkts og kalibreringsværdierne blive sat på businesslaget
        /// </summary>
        public void CheckLimit()
        {
            _limitReady.WaitOne();
                try
                {
                    _businessController.SetLimitVals(_limitVals);
                    if (_limitVals.CalVal != 0)
                    {
                        _businessController.CalibrationValue = _limitVals.CalVal; 
                    }

                    if (_limitVals.ZeroVal != 0)
                    {
                        _businessController.SetZeroAdjust(_limitVals.ZeroVal); 
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
        }
        /// <summary>
        /// når der er kommet en ny komando bliver denne metode kaldt og herfra vil der blive påbegyndt en blodtryksmåling, en nulpunktsjustering, en kalibreringsreferenceværdi, en mute, en stop eller en system slukket, alt efter hvad kommandoen beder om
        /// </summary>
        public void CheckCommands()
        {
            _commandReady.WaitOne();
            try
            {
                switch (commandsPc)
                {
                    case "Startmeasurement":
                        {
                            _businessController.StartMonitoring = true;
                            Thread measurementThread = new Thread(_businessController.RunMeasurement);
                            Thread processingThread = new Thread(_businessController.StartProcessing);
                            measurementThread.Start();
                            processingThread.Start();
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
        }

    }
}