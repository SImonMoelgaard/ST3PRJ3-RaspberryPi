﻿using System;
using DTO_s;
namespace DataAccessLogic
{
    public class SendUI
    {
        private double _zeroAdjustMean;
        /// <summary>
        /// Denne metode sender besked til UI, hvis grænseværdierne bliver overskredet
        /// </summary>
        public void LimitValAreExceeded()
        {

        }

        /// <summary>
        /// denne metode sender et DTO_calculated objekt til UI TODO Hvor ofte????
        /// </summary>
        public void SendCalculatedData()
        {

        }


        /// <summary>
        /// denne metode sender et DTO_Raw objekt til UI TODO Hvor ofte????
        /// </summary>
        public void SendRawData()
        {

        }

        /// <summary>
        /// denne metode sender Hvor meget batteri der er på MI til UI TODO Hvor ofte????
        /// </summary>
        public void SendBatteryStatus()
        {

        }


        /// <summary>
        /// denne metode sender kalibrationsværdien til UI TODO Hvor ofte????
        /// </summary>
        public void SendMeanCalibration()
        {

        }

        /// <summary>
        /// denne metode sender et nulpunktjustering til UI TODO Er denne nødvendig?? Hvad skal UI bruge den til??
        /// </summary>
        public void SendZeroAdjust(double zeroAdjustMean)
        {
            _zeroAdjustMean = zeroAdjustMean;
            Console.WriteLine("Nulpunktsværdien: " + _zeroAdjustMean + " sendes til PC");
        }

    }
}