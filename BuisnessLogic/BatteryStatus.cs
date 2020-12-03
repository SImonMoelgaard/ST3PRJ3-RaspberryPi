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
            //4.37 V er max og den lavete værdi er 1.82( der skal muligvis alarmeres lidt før der er 20 procent tilbage)
            _batteryStatus = Convert.ToInt32(battery * (10 / 7 * 100));
            return _batteryStatus;
            
        }
    }
}
