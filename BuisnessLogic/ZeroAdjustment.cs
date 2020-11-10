using System;
using System.Collections.Generic;
using System.Text;
using DTO_s;
using DataAccessLogic;


namespace BuisnessLogic
{
    public class ZeroAdjustment
    {
        /// <summary>
        /// receiveADC objekt til at få adgang til målingen til nulpunktjusteringen
        /// </summary>
        private ReceiveADC adcObj = new ReceiveADC();
        /// <summary>
        /// nulpunktjusteringen
        /// </summary>
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
