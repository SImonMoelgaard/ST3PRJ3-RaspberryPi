using System;
using System.Collections.Generic;
using System.Text;
using DTO_s;

namespace DataAccessLogic
{
    class ReceiveADC
    {

        //private ADC1015 adc;
        
        //denne metode modtager siganalet (enten blodtryks eller kalibrerins) fra adcen
        public double MeassureSignal()
        {
            //Denne return værdi er kun sat midlertidigt for at undgå fejl
            return 0;
        }

        //denne metode modtager batteriets værdi
        public double MeasureBattery()
        {
            //Denne return værdi er kun sat midlertidigt for at undgå fejl
            return 0;
        }
    }
}
