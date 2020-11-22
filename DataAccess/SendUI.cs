using System;
using DTO_s;
namespace DataAccessLogic
{
    public class SendUI
    {
        private double _zeroAdjustMean;
        private double _calValMean;
        /// <summary>
        /// Denne metode sender besked til UI, hvis grænseværdierne bliver overskredet
        /// </summary>
        public void LimitValAreExceeded()
        {

        }

        /// <summary>
        /// denne metode sender et DTO_calculated objekt til UI TODO Hvor ofte????
        /// </summary>
        public void SendCalculatedData(DTO_Calculated calculated)
        {
            //UDP kode, der sender calculated til PCen
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
        public void SendBatteryStatus(int batteryStatus)
        {
            
        }


        /// <summary>
        /// denne metode sender kalibrationsværdien til UI TODO Hvor ofte????
        /// </summary>
        public void SendCalVal(double meanVal)
        {
            _calValMean = meanVal;
            Console.WriteLine("Kalibreringsværdien: " + _calValMean + "sendes til PC");
            //Mangler UDP 
        }

        /// <summary>
        /// denne metode sender et nulpunktjustering til UI TODO Er denne nødvendig?? Hvad skal UI bruge den til??
        /// </summary>
        public void SendZeroAdjust(double zeroAdjustMean)
        {
            _zeroAdjustMean = zeroAdjustMean;
            Console.WriteLine("Nulpunktsværdien: " + _zeroAdjustMean + " sendes til PC");
            //Mangler UDP
        }

        public void SendExceedVals(DTO_exceedVals limitValExceeded)
        {

        }
    }
}
