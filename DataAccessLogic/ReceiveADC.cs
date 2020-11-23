using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Text;
using DTO_s;
using RaspberryPiCore.ADC;

namespace PresentationLogic
{
    public class ReceiveAdc : IBPData
    {
        /// <summary>
        /// opretter en DTO_raw objekt, som kan sendes videre
        /// </summary>
        private DTO_Raw raw;

        /// <summary>
        /// atribut, der kan sendes med raw objektet
        /// </summary>
        private double mV;

        private ADC1015 adc;
        private  List<double> zeroAdjustVals= new List<double>(10);
        private List<double> calibrationVals= new List<double>(10); 

        public ReceiveAdc()
        {
            adc= new ADC1015();
            
        }
        /// <summary>
        /// denne metode modtager siganalet (enten blodtryks eller kalibrerins) fra adcen, og opretter et DTO_Raw objekt
        /// </summary>
        /// <returns>et blodtryk i V i dette øjeblik</returns>
        public double Measure()
        {
            //Kode der sætter mV til den værdi der kommer ind fra acd'en
            //mangler kode
            //nyquist frekvens=91 så samplefrekvens er 182 Hz
            return mV; 
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
        /// Metode til kalibrering der laver 1 måling over x sekunder og returnerer en double-værdi 
        /// </summary>
        /// <returns></returns>
        public List<double> MeasureCalibration()
        {
            int count = 0;
            while (count!=10)
            {
                double calibrationVal = adc.readADC_SingleEnded(0);
                calibrationVals.Add(calibrationVal);
                count++;
            }

            count = 0;
            return calibrationVals;
            // denne er ikke færdig endnu... 
        }
        /// <summary>
        /// Modtager og returnerer 10 målinger til nulpunktsjustering
        /// </summary>
        /// <returns> liste med 10 målinger </returns>
        public List<double> StartZeroAdjust()
        {
            int count = 0;
            while (count != 10)
            {
                double measureVal = adc.readADC_SingleEnded(0); //Skal der gøres noget ved de værdier eller kan de lægges direkte ind i listen??
                zeroAdjustVals.Add(measureVal);
                count++;
            }
            count = 0;
            return zeroAdjustVals;
        }
    }
}
