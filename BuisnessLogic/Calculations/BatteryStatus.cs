using System;
using System.Collections.Generic;
using System.Text;
using DTO_s;
using DataAccessLogic;

namespace BusinessLogic
{
    public class BatteryStatus
    {
        
        private int _batteryStatus;
        /// <summary>
        /// Udregner batteristatusen i intervaller
        /// </summary>
        /// <returns>batterystatus</returns>
        public int CalculateBatteryStatus(double battery)
        { 
            if (battery < 4.37 && battery >= 4.17) _batteryStatus = 100;
            if (battery < 4.17 && battery >= 4.02) _batteryStatus = 90;
            if (battery < 4.02 && battery >= 3.8) _batteryStatus = 80;
            if (battery < 3.8 && battery >= 3.66) _batteryStatus = 70;
            if (battery < 3.66 && battery >= 3.44) _batteryStatus = 60;
            if (battery < 3.44 && battery >= 3.31) _batteryStatus = 50;
            if (battery < 3.31 && battery >= 3.06) _batteryStatus = 40;
            if (battery < 3.06 && battery >= 2.91) _batteryStatus = 30;
            if (battery < 2.91 && battery >= 2.7) _batteryStatus = 20;
            if (battery < 2.7 && battery >= 2.54) _batteryStatus = 10;
            if (battery < 2.54) _batteryStatus = 0;
            return _batteryStatus;
        }
    }
}