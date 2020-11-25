using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Text;
using DTO_s;
using RaspberryPiCore.ADC;


namespace DataAccessLogic
{
    public class ReceiveAdc : IBPData
    {
       
        /// <summary>
        /// atribut, der kan sendes med raw objektet
        /// </summary>
        private readonly ADC1015 _adc;
        private readonly  List<double> _zeroAdjustVals= new List<double>(10);
        private readonly List<double> calibrationVals= new List<double>(10);
       

        public ReceiveAdc()
        {
            _adc= new ADC1015();
            
        }
        /// <summary>
        /// denne metode modtager siganalet (enten blodtryks eller kalibrerins) fra adcen, og opretter et DTO_Raw objekt
        /// </summary>
        /// <returns>et blodtryk i V i dette øjeblik</returns>
        public double Measure()
        {
            //Kode der sætter _mV til den værdi der kommer ind fra acd'en
            //mangler kode
            while (true) //overvejer der skal være en bool der sendes med som true, og når man så trykker på stop, ændres bool til false, stopper denne løkke
            {
                double measureVal = _adc.readADC_Differential_0_1();
                return measureVal;
            }
            
            //nyquist frekvens=91 så samplefrekvens er 182 Hz
            // muligvis ikke færdig 
        }
        /// <summary>
        /// Denne metode modtager batteriets kapacitet
        /// </summary>
        /// <returns>hvor meget batteri, der er tilbage på MI</returns>
        public double MeasureBattery()
        {
            double measureBattery = _adc.readADC_SingleEnded(2);
            return measureBattery;
        }
        /// <summary>
        /// Metode til kalibrering der laver 1 måling over x sekunder og returnerer en double-værdi 
        /// </summary>
        /// <returns></returns>
        public List<double> MeasureCalibration(List<double> calDoubles)
        {
            int count = 0;
            int measureTime = 5 * 182; //måler i 5 sekunder
            while (count!=measureTime)
            {
                double calibrationVal = _adc.readADC_Differential_0_1();
                calDoubles.Add(calibrationVal);
                count++;
            }

            return calDoubles;
           
        }
        /// <summary>
        /// Modtager og returnerer 10 målinger til nulpunktsjustering
        /// </summary>
        /// <returns> liste med 10 målinger </returns>
        public List<double> MeasureZeroAdjust(List<double> zeroDoubles)
        {
            int count = 0;
            int measureTime = 5 * 182; //måler i 5 sekunder
            while (count != measureTime)
            {
                double measureVal = _adc.readADC_Differential_0_1(); //Skal der gøres noget ved de værdier eller kan de lægges direkte ind i listen??
                zeroDoubles.Add(measureVal);
                count++;
            }
            return zeroDoubles; 
        }
    }
}
