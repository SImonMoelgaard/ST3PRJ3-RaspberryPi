using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO_s;
using DataAccessLogic;


namespace BusinessLogic
{
    /// <summary>
    /// klasse til udregning af nulpunktsjusteringsværdien
    /// </summary>
    public class ZeroAdjustment
    {

        
        private double zeroAdjustMean;

        /// <summary>
        /// Udregner værdierne til nulpunktsjustering
        /// </summary>
        /// <returns>den udregnede nulpunktsjusteringsværdi</returns>
        public double CalculateZeroAdjustMean(List<double> zeroAdjustVals)
        {
            zeroAdjustMean = zeroAdjustVals.Average();
            return zeroAdjustMean;
        }
    }
}