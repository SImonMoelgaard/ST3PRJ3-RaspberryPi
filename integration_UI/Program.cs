using RaspberryPiNetCore.ADC;
using RaspberryPiNetCore.LCD;
using RaspberryPiNetCore.TWIST;
using System;
using System.Collections.Generic;
using System.Threading;
using DataAccessLogic;
using DTO_s;


namespace Raspberry_Pi_Dot_Net_Core_Console_Application3
{
    class Program
    {
        private static UdpListener udpListener= new UdpListener();
        private static UdpSender udpSender= new UdpSender();
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine("Test af commands- Listener");
            var command= udpListener.ListenCommandsPC();
            Console.WriteLine("UI siger: " + command);
            Console.WriteLine("Test af LimitVals- Listener, tryk enter");
            Console.ReadLine();
            var limitVals = udpListener.ListenLimitValsPC();
            var highSys = limitVals.HighSys;
            var lowSys = limitVals.LowSys;
            var highDia = limitVals.HighDia;
            var lowDia = limitVals.LowDia;
            var zeroVal = limitVals.ZeroVal;
            var calVal = limitVals.CalVal;
            var highMean = limitVals.HighMean;
            var lowMean = limitVals.LowMean;
            var nl = "\r\n";
            Console.WriteLine("HighSys: " + highSys + nl + "lowSys: " + lowSys + nl +"HighDia: "+ highDia + nl + "LowDia: " + lowDia+ nl + "HighMean: " + highMean + "LowMean: " + lowMean+nl +"ZeroVal: "+ zeroVal + nl + "CalVal: "+ calVal + nl   );
            Console.WriteLine("Test af SendDouble");
            Console.ReadLine();
            var value = 5.5;
            udpSender.SendDouble(value);
            Console.WriteLine("Værdien er nu sendt");
            Console.WriteLine("Tryk for test af SendDTO_Calculated");
            Console.ReadLine();
            var calculated= new DTO_Calculated(true, true, true, true, true, true, 120, 60,80,70,55);
           udpSender.SendDTO_Calculated(calculated);
            Console.WriteLine("Værdierne er sendt");
            Console.WriteLine("Tryk for test af SendDTO_Raw");
            Console.ReadLine();
            Random random= new Random();
            var list = new List<DTO_Raw>();
            for (int i = 0; i < 45; i++)
            {
                var raw = new DTO_Raw(random.NextDouble(), DateTime.Now);
                list.Add(raw);
            }
            
            udpSender.SendDTO_Raw(list);
            Console.WriteLine("Data er nu sendt");
            Console.ReadLine();


        }
    }
}
