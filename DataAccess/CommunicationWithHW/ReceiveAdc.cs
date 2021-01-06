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
        private readonly ADC1015 _adc = new ADC1015(72, 1);
        private const int fivesec = 175 * 5;
        private List<double> _zeroAdjustVals;
        private List<double> _calibrationVals;
        private DTO_Raw raw;


              
        /// <summary>
        /// denne metode modtager siganalet (enten blodtryks eller kalibrerins) fra adcen, og opretter et DTO_Raw objekt
        /// </summary>
        /// <returns>et blodtryk i V i dette øjeblik</returns>
        public DTO_Raw Measure()
        {
            //_adc= new ADC1015(72,1)

            var measureVal = _adc.readADC_SingleEnded(0);
            raw = new DTO_Raw(measureVal, DateTime.Now);
            return raw;

            //nyquist frekvens=91 så samplefrekvens er 182 Hz
        }
        /// <summary>
        /// Denne metode modtager batteriets kapacitet
        /// </summary>
        /// <returns>hvor meget batteri, der er tilbage på MI</returns>
        public double MeasureBattery()
        {

            ushort measureBattery = _adc.readADC_SingleEnded(2);
            return measureBattery;
        }

        /// <summary>
        /// Metode til kalibrering der laver 1 måling over x sekunder og returnerer en double-værdi 
        /// </summary>
        /// <returns></returns>


    

        public List<double> MeasureCalibration()
        {
            _calibrationVals = new List<double>(fivesec);
            int count = 0; ; //måler i 5 sekunder
            while (count <= fivesec)
            {
                var calibrationVal = Convert.ToDouble(_adc.readADC_SingleEnded(0));
                _calibrationVals.Add(calibrationVal);
                count++;
            }

            return _calibrationVals;

        }
        /// <summary>
        /// Modtager og returnerer 10 målinger til nulpunktsjustering
        /// </summary>
        /// <returns> liste med 10 målinger </returns>
        public List<double> MeasureZeroAdjust()
        {
            _zeroAdjustVals = new List<double>(fivesec);
            int count = 0;
            //int measureTime = 5 * 175; //måler i 5 sekunder
            while (count <= fivesec)
            {
                var measureVal = Convert.ToDouble(_adc.readADC_SingleEnded(0)); 
                _zeroAdjustVals.Add(measureVal);
                count++;
            }
            return _zeroAdjustVals;
        }

    }
}
