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
    public class IndicateBattery
    {

        //SoftPmw softpmw= new SoftPmw(); 
        //GPIO ledGpio= new GPIO();
        private GpioController _gpioController;

        private const int _batteryLed = 18
;
        /// <summary>
        /// atribut, der definere LED'en, der skal indikere Batteristatus 
        /// </summary>
        // private LED BatteryLED;
        /// <summary>
        ///Får LED'en til at blinke hvis batteristatusen er under xx% 
        /// </summary>
        public IndicateBattery()
        {
            _gpioController = new GpioController();
            _gpioController.OpenPin(_batteryLed, PinMode.Output);
        }

        public void IndicateLowBattery()
        {
            _gpioController.Write(_batteryLed, PinValue.High);
        }

        public void TurnOff()
        {
            _gpioController.Write(_batteryLed, PinValue.Low);
        }
    }
}