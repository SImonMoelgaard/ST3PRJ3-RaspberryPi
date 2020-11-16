using System;
using System.Collections.Generic;
using System.Text;
using RaspberryPiCore;
using DTO_s;
using BusinessLogic;

namespace Presentation_Logic
{
    class IndicateBattery
    {
        /// <summary>
        /// atribut, der definere LED'en, der skal indikere Batteristatus 
        /// </summary>
        private Led BatteryLED;
        /// <summary>
        ///Får LED'en til at blinke hvis batteristatusen er under xx% 
        /// </summary>
        public void IndicateLowBattery()
        {

        }
    }
}
