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
        public int CalculateBatteryStatus(double battery)
        {
            //det her er ikke den rigtige udregning - bare en ca
            _batteryStatus = Convert.ToInt32(battery * (10 / 7 * 100));
            return _batteryStatus;
            
        }
    }
}
