using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Dynamic;
using System.Text;
using RaspberryPiCore;
using DTO_s;
//using RaspberryPiNetCore.WiringPi;



namespace DataAccessLogic
{
    /// <summary>
    /// kommunikerer med hw, der skal tænde en LED når batteristatus er lav 
    /// </summary>
    public class IndicateBattery
    {

        //SoftPmw softpmw= new SoftPmw(); 
        //GPIO ledGpio= new GPIO();
        private GpioController _gpioController;

        private const int _batteryLed = 18
;

        public IndicateBattery()
        {
            _gpioController = new GpioController();
            _gpioController.OpenPin(_batteryLed, PinMode.Output);
        }

        /// <summary>
        ///Får LED'en til at blinke hvis batteristatusen er under xx% 
        /// </summary>
        public void IndicateLowBattery()
        {
            _gpioController.Write(_batteryLed, PinValue.High);
        }
        /// <summary>
        /// slukker LE*D'en når denne er okay igen
        /// </summary>
        public void TurnOff()
        {
            _gpioController.Write(_batteryLed, PinValue.Low);
        }
    }
}