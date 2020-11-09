using System;
using System.Collections.Generic;
using System.Text;
using DTO_s;

namespace DataAccessLogic
{
    public class ReceiveADC
    {
        private DTO_Raw raw;

        private double mV;
        //private ADC1015 adc;
        
        //denne metode modtager siganalet (enten blodtryks eller kalibrerins) fra adcen
        public DTO_Raw MeassureSignal()
        {
            

            //Kode der sætter mV til den værdi der kommer ind fra acd'en 
            return raw=new DTO_Raw(mV,DateTime.Now);
        }

        //denne metode modtager batteriets værdi
        public double MeasureBattery()
        {
            //Denne return værdi er kun sat midlertidigt for at undgå fejl
            return 0;
        }
    }
}
