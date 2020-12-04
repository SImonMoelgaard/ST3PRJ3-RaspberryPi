using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Dynamic;
using System.Text;
using RaspberryPiCore;
using DTO_s;
using RaspberryPiNetCore.WiringPi;



namespace DataAccessLogic
{
    class IndicateBattery
    {

        SoftPmw softpmw= new SoftPmw(); 
        GPIO ledGpio= new GPIO();
        /// <summary>
        /// atribut, der definere LED'en, der skal indikere Batteristatus 
        /// </summary>
       // private LED BatteryLED;
        /// <summary>
        ///Får LED'en til at blinke hvis batteristatusen er under xx% 
        /// </summary>
        
        public void IndicateLowBattery()
        {
           //var led= softpmw.Create(18,0,100);
            ledGpio.PinMode(18, 1);
          

        }
    }
}
