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
        private double meanVal;
        
        /// <summary>
        /// udregner calval ved at finde gennemsnittet af listen, der kommer med
        /// </summary>
        /// <param name="calVals">liste over måleværdier</param>
        /// <param name="zeroPointAdjust">nulpunktjustering, der tager højde for det atmosfæriske tryk</param>
        /// <returns></returns>
        public double CalculateMeanVal(List<double> calVals, double zeroPointAdjust)
        {
            meanVal = calVals.Average();
            meanVal = meanVal - zeroPointAdjust;
            return meanVal;
        }
    }
}