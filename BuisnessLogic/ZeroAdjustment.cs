using System;
using System.Collections.Generic;
using System.Text;
using DTO_s;
using DataAccessLogic;


namespace BuisnessLogic
{
    public class ZeroAdjustment
    {
        private ReceiveADC adcObj = new ReceiveADC();
        private double calculatedZeroVal;


        /// <summary>
        /// Udregner værdierne til nulpunktsjustering
        /// </summary>
        /// <returns>den udregnede nulpunktsjusteringsværdi</returns>
        public double CalculateZeroVal()
        {

            return calculatedZeroVal;
        }
    }
}
