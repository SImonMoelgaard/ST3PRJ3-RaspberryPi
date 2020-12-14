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
            const double min = 1.82;
            const double max = 4.37;
            _batteryStatus = Convert.ToInt32((battery - min) / (max - min) * 100);
            return _batteryStatus;

        }
    }
}