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
        }

        public void SendDTO_Raw(List<DTO_Raw> dtoRaw)
        {
            Console.WriteLine("Send Raw");
        }

        public void SendDouble(double meanVal)
        {
            Console.WriteLine("Send double");
        }
    }

}
