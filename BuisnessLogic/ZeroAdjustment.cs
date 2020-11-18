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

        private List<double> _zeroAdjustVals= new List<double>();


        /// <summary>
        /// Udregner værdierne til nulpunktsjustering
        /// </summary>
        /// <returns>den udregnede nulpunktsjusteringsværdi</returns>
        public double CalculateZeroAdjustMean(List<double> zeroAdjustVals)
        {
            _zeroAdjustVals = zeroAdjustVals;
            zeroAdjustMean = _zeroAdjustVals.Average();
            return zeroAdjustMean;
        }
    }
}
