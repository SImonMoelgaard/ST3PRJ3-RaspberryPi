using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DTO_s;

namespace DataAccessLogic
{
    class FakeSender : ISender

    {
        public void SendDTO_Calculated(DTO_Calculated dtoCalculated)
        {
            Console.WriteLine("Send Calculated");
            Console.WriteLine(dtoCalculated.HighSys);
            Console.WriteLine(dtoCalculated.LowSys);
            Console.WriteLine(dtoCalculated.HighDia);
            Console.WriteLine(dtoCalculated.LowDia);
            Console.WriteLine(dtoCalculated.Batterystatus);
            Console.WriteLine(dtoCalculated.HighMean);
            Console.WriteLine(dtoCalculated.LowMean);
            Console.WriteLine(dtoCalculated.CalculatedSys);
            Console.WriteLine(dtoCalculated.CalculatedDia);
            Console.WriteLine(dtoCalculated.CalculatedMean);
            Console.WriteLine(dtoCalculated.CalculatedPulse);
        }

        public void SendDTO_Raw(List<DTO_Raw> dtoRaw)
        {
            throw new NotImplementedException();
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
