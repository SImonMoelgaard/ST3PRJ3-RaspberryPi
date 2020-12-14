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
        private double zeroAdjustMean;

        /// <summary>
        /// Udregner værdierne til nulpunktsjustering
        /// </summary>
        /// <returns>den udregnede nulpunktsjusteringsværdi</returns>
        public double CalculateZeroAdjustMean(List<double> zeroAdjustVals)
        {
            zeroAdjustMean = zeroAdjustVals.Average();
            zeroAdjustMean = (zeroAdjustMean / 559 / 5 / 0.000005);
            return zeroAdjustMean;
        }
    }
}