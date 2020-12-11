using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DTO_s;

namespace DataAccessLogic
{
    class FakeSender : ISender

    {
        int count = 0; 
        public void SendDTO_Calculated(DTO_Calculated dtoCalculated)
        {
            Console.WriteLine("Send Calculated");
            Console.WriteLine("Highsys: "+dtoCalculated.HighSys);
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

        public void SendDTO_Raw(List<DTO_Raw> dtoRaw)
        {
            //foreach (var item in dtoRaw)
            //{
            //    Console.WriteLine("Måling: "+item.mmHg);
            //}
           // count++;
            //if (count == 20)
           // {
                Console.WriteLine("DataSendt");
                //count = 0;
            //}
        }

        //public void SendDTO_Raw(DTO_Raw dtoRaw)
        //{
        //    Console.WriteLine("Send Raw");
        //    Console.WriteLine(dtoRaw.mmHg);
        //    Console.WriteLine(dtoRaw.Tid);
        //}

        public void SendDouble(double meanVal)
        {
            Console.WriteLine("Send double");
        }
    }

}
