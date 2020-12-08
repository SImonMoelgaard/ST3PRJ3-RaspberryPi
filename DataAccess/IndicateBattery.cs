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
        private static GpioController _gpioController;

        private const int _batteryLed = 4;
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
           //var led= softpmw.Create(18,0,100);
            //ledGpio.PinMode(18, 1);
          
         _gpioController.Write(_batteryLed,PinValue.High);
        }

        public void TurnOff()
        {
            _gpioController.Write(_batteryLed,PinValue.Low);
        }
    }
}
