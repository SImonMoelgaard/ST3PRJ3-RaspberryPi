using System;
using System.Collections.Generic;
using System.Text;
using DTO_s;
using DataAccessLogic;

namespace BusinessLogic
{
    public class BatteryStatus
    {
        /// <summary>
        /// indikere hvor meget batteri der er tilbage i MI
        /// </summary>
        private int _batteryStatus;
        /// <summary>
        /// Udregner batteristatusen TODO How?
        /// </summary>
        /// <returns>batterystatus</returns>
        public int CalculateBatteryStatus()
        {
            return _batteryStatus;
            
        }
    }
}
