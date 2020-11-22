using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO_s;
using DataAccessLogic;

namespace BusinessLogic
{
    public class Calibration
    {
        /// <summary>
    /// gennemsnitsværdien af kalibreringsværdien
    /// </summary>
        private double meanVal;
        /// <summary>
        /// Udregner en middelværdi til kalibreringen, udfra målinger ud til et givent atmosfærisk tryk
        /// </summary>
        /// <returns>meanVal</returns>

        public double CalculateMeanVal(List<double> calVals)
        {
            meanVal = calVals.Average();
            return meanVal;
        }
    }
}
