using System;
using System.Collections.Generic;
using System.Text;
using DTO_s;
using DataAccessLogic;


namespace BusinessLogic
{
    public class ZeroAdjustment
    {
        /// <summary>
        /// receiveADC objekt til at få adgang til målingen til nulpunktjusteringen
        /// </summary>
        //private ReceiveAdc adcObj = new ReceiveAdc(); TROR iKKE DENNE BLIVER NØDVENDIG LÆNGERE 
        /// <summary>
        /// nulpunktjusteringen
        /// </summary>
        private double zeroAdjustMean;


        /// <summary>
        /// Udregner værdierne til nulpunktsjustering
        /// </summary>
        /// <returns>den udregnede nulpunktsjusteringsværdi</returns>
        public double CalculateZeroAdjustMean()
        {
           
            return zeroAdjustMean;
        }
    }
}
