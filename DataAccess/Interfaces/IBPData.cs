using System;
using System.Collections.Generic;
using System.Text;
using DTO_s;

namespace DataAccessLogic
{
    /// <summary>
    /// Dette er et Interface, der gør det nemmere at skifte mellem Physionets måling og fra ADC'en
    /// Senere vil denne også gøre det nemmere at teste klasserne
    /// </summary>
    public interface IBPData
    {
        /// <summary>
        /// starter lytningen til adcen
        /// </summary>
        /// <returns>målinger af blodtrykket</returns>
        DTO_Raw Measure();
        /// <summary>
        /// lytter efter batteristatusen
        /// </summary>
        /// <returns>bateristatusen</returns>
        double MeasureBattery();
      /// <summary>
      /// laver en liste over 5 sekunder til udregning af en kalibreringsværdi 
      /// </summary>
      /// <returns>liste af målinger</returns>
        List<double> MeasureCalibration();
      /// <summary>
      /// laver en liste over 5 sekunder til udregning af en nulpunktsværdi 
      /// </summary>
      /// <returns>liste af målinger</returns>
        List<double> MeasureZeroAdjust();
        
    }
}