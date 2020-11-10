using System;
using System.Collections.Generic;
using System.Text;
using DTO_s;

namespace DataAccessLogic
{
    public class ReceiveADC : IBPData
    {
        /// <summary>
        /// opretter en DTO_raw objekt, som kan sendes videre
        /// </summary>
        private DTO_Raw raw;

        /// <summary>
        /// atribut, der kan sendes med raw objektet
        /// </summary>
        private double mV;
        //private ADC1015 adc;
        private List<double> zeroAdjustVal;

        /// <summary>
        /// denne metode modtager siganalet (enten blodtryks eller kalibrerins) fra adcen, og opretter et DTO_Raw objekt
        /// </summary>
        /// <returns>et blodtryk i V i dette øjeblik</returns>
        public DTO_Raw MeassureSignal()
        {
            //Kode der sætter mV til den værdi der kommer ind fra acd'en 
            return raw=new DTO_Raw(mV,DateTime.Now);
        }
        /// <summary>
        /// Denne metode modtager batteriets kapacitet
        /// </summary>
        /// <returns>hvor meget batteri, der er tilbage på MI</returns>
        public double MeasureBattery()
        {
            //Denne return værdi er kun sat midlertidigt for at undgå fejl
            return 0;
        }

        /// <summary>
        /// Modtager og returnerer 10 målinger til nulpunktsjustering
        /// </summary>
        /// <returns> liste med 10 målinger </returns>
        public List<double> StartZeroAdjust()
        {
            return zeroAdjustVal;
        }
    }
}
