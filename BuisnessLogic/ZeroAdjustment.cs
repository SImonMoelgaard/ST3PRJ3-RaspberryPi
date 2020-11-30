using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO_s;
using DataAccessLogic;


namespace BusinessLogic
{
    public class ZeroAdjustment
    {

        /// <summary>
        /// nulpunktjusteringen
        /// </summary>
        public double ZeroAdjustMean { get; set; }
        
        /// <summary>
        /// Udregner værdierne til nulpunktsjustering
        /// </summary>
        /// <returns>den udregnede nulpunktsjusteringsværdi</returns>
        public double CalculateZeroAdjustMean(List<double> zeroAdjustVals)
        {
            ZeroAdjustMean = zeroAdjustVals.Average();
            return ZeroAdjustMean;
        }
    }
}
