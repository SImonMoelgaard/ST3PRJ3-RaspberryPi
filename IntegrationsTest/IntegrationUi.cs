using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using DataAccessLogic;
using DTO_s;
using System.Collections.Concurrent;

namespace IntegrationsTest
{
    class IntegrationUi
    {

        private static UdpListener udpListener;
        private static UdpSender udpSender;

        public IntegrationUi(BlockingCollection<DataContainerUdp> dataQueueCommand, BlockingCollection<DataContainerUdp> dataQueueLimit)
        {
            udpListener = new UdpListener(dataQueueCommand,dataQueueLimit);
            udpSender = new UdpSender();
        }

        public void TrådTestSenderRawAndCalculated()
        {
            TestUdp tester = new TestUdp();
            Thread thread1 = new Thread(SendCalculatedTest);
            Thread thread2 = new Thread(SendRawTest);
            thread1.Start();
            thread2.Start();           
        }

        public void ListenerCommandTest()
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine("Test af commands- Listener");
            var command = "";
            while (true)
            {
                udpListener.ListenCommandsPC();
                command = udpListener.Command;
                Console.WriteLine("UI siger: " + command);
            }
        }
       public void LimitValsTest()
        {
            Console.WriteLine("Test af LimitVals- Listener, tryk enter");
            Console.ReadLine();
            bool test = true;
            DTO_LimitVals limitVals = new DTO_LimitVals(0, 0, 0, 0, 0, 0, 0, 0);
            while (test)
            {
                udpListener.ListenLimitValsPC();
                limitVals = udpListener.DtoLimit;
                var highSys = limitVals.HighSys;
                var lowSys = limitVals.LowSys;
                var highDia = limitVals.HighDia;
                var lowDia = limitVals.LowDia;
                var zeroVal = limitVals.ZeroVal;
                var calVal = limitVals.CalVal;
                var highMean = limitVals.HighMean;
                var lowMean = limitVals.LowMean;
                var nl = "\r\n";
                Console.WriteLine("HighSys: " + highSys + nl + "lowSys: " + lowSys + nl + "HighDia: " + highDia + nl + "LowDia: " + lowDia + nl + "HighMean: " + highMean + "LowMean: " + lowMean + nl + "ZeroVal: " + zeroVal + nl + "CalVal: " + calVal + nl);
            }

        }

        public void SendDoubleTest()
        {
            Console.WriteLine("Test af SendDouble");
            Console.ReadLine();
            var value = 5.5;
            udpSender.SendDouble(value);
            Console.WriteLine("Værdien er nu sendt");
        }
        public void SendCalculatedTest()
        {
            var calculated = new DTO_Calculated(true, true, true, true, true, true, 120, 60, 80, 70, 55, DateTime.Now);
            udpSender.SendDTO_Calculated(calculated);
            Console.WriteLine("Værdierne er sendt");
        }

        public void SendRawTest()
        {
            Random random = new Random();
            var rawList = new List<DTO_Raw>();
            for (int i = 0; i < 10; i++)
            {
                var dtoObj = new DTO_Raw(random.Next(),DateTime.UtcNow);

                rawList.Add(dtoObj);
            }

            udpSender.SendDTO_Raw(rawList);
        }


       


    }
}
