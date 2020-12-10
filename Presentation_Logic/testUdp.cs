﻿using System;
using System.Collections.Generic;

using System.Text;
using System.Threading;
using DataAccessLogic;
using DTO_s;

namespace BP_program
{
    public class TestUdp
{
        UdpSender udpSender = new UdpSender();
        public void TestCalculated()
        {
            Console.ReadLine();
            Random random = new Random();


            while (true)
            {
                //var list = new List<DTO_Raw>();
                var raw = new DTO_Calculated(true, true, true, true, true, true, random.Next(80, 120), random.Next(80, 120), random.Next(80, 120), random.Next(80, 120), random.Next(80, 120), DateTime.UtcNow);
                udpSender.SendDTO_Calculated(raw);
                Thread.Sleep(5000);

            }

        }

        public void TestRaw()
        {
            Random random1 = new Random();

            while (true)
            {
                //var list = new List<DTO_Raw>();
                var raw = new DTO_Raw(random1.Next(80, 120), DateTime.UtcNow);
                udpSender.SendDTO_Raw(raw);
                Thread.Sleep(5);
            }

        }
    }
}