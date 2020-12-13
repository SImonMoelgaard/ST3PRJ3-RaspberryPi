using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Text;
using DTO_s;
using RaspberryPiCore.ADC;
using RaspberryPiCore; 


namespace DataAccessLogic
{
    public class ReceiveAdc : IBPData
    {
       
        /// <summary>
        /// atribut, der kan sendes med raw objektet
        /// </summary>
        private static ADC1015 _adc;
        private readonly  List<double> _zeroAdjustVals= new List<double>(875);
        private readonly List<double> calibrationVals= new List<double>(875);
       

        public ReceiveAdc()
        {
            _adc= new ADC1015(72,1);
            
        }
        /// <summary>
        /// denne metode modtager siganalet (enten blodtryks eller kalibrerins) fra adcen, og opretter et DTO_Raw objekt
        /// </summary>
        /// <returns>et blodtryk i V i dette øjeblik</returns>
        public double Measure()
        {
            //_adc= new ADC1015(72,1)
            var measureVal = (_adc.readADC_SingleEnded(0)/*/2048.0*/ )/**6.144*/ ;
            //Console.WriteLine("mål fra adc: "+measureVal );
            //Console.WriteLine(_adc.readADC_Differential_0_1());
            //Console.WriteLine(_adc.readADC_Differential_0_1());
            //short tmeasureVal = _adc.readADC_Differential_0_1();
            //double measureVal = tmeasureVal;
            
                return measureVal;

                //nyquist frekvens=91 så samplefrekvens er 182 Hz
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
        /// Metode til kalibrering der laver måling over 5 sekunder og returnerer en liste af double 
        /// </summary>
        /// <returns></returns>
        public List<double> MeasureCalibration()
        {
            int count = 0;
            int measureTime = 5 * 175; //måler i 5 sekunder
            while (count!=measureTime)
            {
                double calibrationVal = _adc.readADC_SingleEnded(0);
                    //_adc.readADC_Differential_0_1();
                calibrationVals.Add(calibrationVal);
                count++;
            }

            return calibrationVals;
           
        }
        /// <summary>
        /// Modtager og returnerer en liste af doubles fra ADC'en over 5 sekunder til nulpunktsjustering
        /// </summary>
        /// <returns> liste med 10 målinger </returns>
        public List<double> MeasureZeroAdjust()
        {
            int count = 0;
            int measureTime = 5 *175 ; //måler i 5 sekunder
            while (count != measureTime)
            {
                double measureVal = _adc.readADC_SingleEnded(0) ;
                    //_adc.readADC_Differential_0_1(); //Skal der gøres noget ved de værdier eller kan de lægges direkte ind i listen??
                _zeroAdjustVals.Add(measureVal);
                count++;
            }
            return _zeroAdjustVals; 
        }

        
    }
}
