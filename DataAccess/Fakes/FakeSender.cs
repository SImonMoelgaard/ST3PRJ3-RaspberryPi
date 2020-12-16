using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DTO_s;

namespace DataAccessLogic
{
    /// <summary>
    /// class der skal gøre det ud for UDPsenderen til tests
    /// </summary>
    class FakeSender : ISender

    {
        /// <summary>
        /// sender calculated bestående af sys, dia, middel, puls, batteristatus og om overskridelser af grænseværdier er sket
        /// </summary>
        /// <param name="dtoCalculated">dto calculated</param>
        public void SendDTO_Calculated(DTO_Calculated dtoCalculated)
        {
            Console.WriteLine("Send Calculated");
            Console.WriteLine("Highsys: " + dtoCalculated.HighSys);
            Console.WriteLine("lowsys: " + dtoCalculated.LowSys);
            Console.WriteLine("Highdia: " + dtoCalculated.HighDia);
            Console.WriteLine("lowdia: " + dtoCalculated.LowDia);
            Console.WriteLine("batteristatur: " + dtoCalculated.Batterystatus);
            Console.WriteLine("HighMean: " + dtoCalculated.HighMean);
            Console.WriteLine("LowMean: " + dtoCalculated.LowMean);
            Console.WriteLine("Sys: " + dtoCalculated.CalculatedSys);
            Console.WriteLine("dia: " + dtoCalculated.CalculatedDia);
            Console.WriteLine("mean: " + dtoCalculated.CalculatedMean);
            Console.WriteLine("pulse: " + dtoCalculated.CalculatedPulse);
        }
        /// <summary>
        /// sender liste med dto raw bestående af målinger over et halvt sekund i mmHg med dertilhørende tidspunkt
        /// </summary>
        /// <param name="dtoRaw"></param>
        public void SendDTO_Raw(List<DTO_Raw> dtoRaw)
        {
            foreach (var item in dtoRaw)
            {
                Console.WriteLine("Måling: " + item.mmHg);
            }
        }


        /// <summary>
        /// sender en double
        /// </summary>
        /// <param name="meanVal">enten zeroval eller en kalibreringsværdi</param>
        public void SendDouble(double meanVal)
        {
            Console.WriteLine("Send double" + meanVal);
        }

    }

}