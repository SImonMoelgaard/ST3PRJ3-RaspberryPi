using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using BusinessLogic;
using RaspberryPiCore.ADC;
using RaspberryPiCore.TWIST;
using RaspberryPiCore.LCD;
using BusinessLogic;
using DataAccessLogic;
using RPI;
using Led = RaspberryPi.Led;
using DTO_s;


namespace BP_program
{
    class Program 
    {
        //private static RaspberryPiCore rpi;
        private static RaspberryPi.RaspberryPiDll rpi;


        // til Ui integration 
        private static UdpListener udpListener = new UdpListener();
        private static UdpSender udpSender = new UdpSender();

        //Til hw integration
        private static ReceiveAdc _adc = new ReceiveAdc();
        private static IndicateBattery indicateBattery = new IndicateBattery();
        private static Alarm alarm = new Alarm();

        static void Main(string[] args)
        {
            // DEtte er det rigtige program 
            //Console.WriteLine("Hello World!");

            //BlockingCollection<DataContainerMeasureVals> dataQueueMeasure = new BlockingCollection<DataContainerMeasureVals>();
            //BlockingCollection<DataContainerUdp> dataQueueCommand = new BlockingCollection<DataContainerUdp>();
            //BlockingCollection<DataContainerUdp> dataQueueLimit = new BlockingCollection<DataContainerUdp>();

            //BusinessController businessController = new BusinessController(dataQueueCommand, dataQueueLimit, dataQueueMeasure);

            //PresentationController presentationController = new PresentationController(businessController);

            ////presentationController.RunCommandsTest();
            //Thread consumerCommands = new Thread(presentationController.RunConsumerCommands);
            //Thread consumerLimit = new Thread(presentationController.RunConsumerLimit);
            //Thread listenCommands = new Thread(presentationController.CheckCommands); // Disse skal muligvis kaldes i en metode
            //Thread listenLimitVal = new Thread(presentationController.CheckLimit); // Disse skal muligvis kaldes i en metode 
            //Thread producerCommands = new Thread(presentationController.RunProducerCommands);
            //Thread producerLimits = new Thread(presentationController.RunProducerLimit);


            //producerLimits.Start();
            //producerCommands.Start();
            //consumerCommands.Start();
            //consumerLimit.Start();
            //listenCommands.Start();
            //listenLimitVal.Start();
            //producerCommands.Join();
            //consumerCommands.Join();
            //listenCommands.Join();


            // Dette er rest af UI integration 
            // TRÅDET UDPTEST UI 
            TestUdp tester= new TestUdp();
            Thread thread1= new Thread(tester.TestCalculated);
            Thread thread2= new Thread(tester.TestRaw);
            thread1.Start();
            thread2.Start();

            //Console.WriteLine("Hello World!");
            //Console.WriteLine("Test af commands- Listener");
            //var command = "";
            //while (command == "")
            //{
            //    command = udpListener.ListenCommandsPC();
            //}

            //Console.WriteLine("UI siger: " + command);
            //Console.WriteLine("Test af LimitVals- Listener, tryk enter");
            //Console.ReadLine();
            //bool test = true;
            //DTO_LimitVals limitVals = new DTO_LimitVals(0, 0, 0, 0, 0, 0, 0, 0);
            //while (test)
            //{
            //    limitVals = udpListener.ListenLimitValsPC();
            //    test = false;
            //}

            //var highSys = limitVals.HighSys;
            //var lowSys = limitVals.LowSys;
            //var highDia = limitVals.HighDia;
            //var lowDia = limitVals.LowDia;
            //var zeroVal = limitVals.ZeroVal;
            //var calVal = limitVals.CalVal;
            //var highMean = limitVals.HighMean;
            //var lowMean = limitVals.LowMean;
            //var nl = "\r\n";
            //Console.WriteLine("HighSys: " + highSys + nl + "lowSys: " + lowSys + nl + "HighDia: " + highDia + nl + "LowDia: " + lowDia + nl + "HighMean: " + highMean + "LowMean: " + lowMean + nl + "ZeroVal: " + zeroVal + nl + "CalVal: " + calVal + nl);
            //Console.WriteLine("Test af SendDouble");
            //Console.ReadLine();
            //var value = 5.5;
            //udpSender.SendDouble(value);
            //Console.WriteLine("Værdien er nu sendt");
            //Console.ReadLine();
            //Console.WriteLine("Tryk for test af SendDTO_Calculated");
            //Console.ReadLine();
            //var calculated = new DTO_Calculated(true, true, true, true, true, true, 120, 60, 80, 70, 55);
            //udpSender.SendDTO_Calculated(calculated);
            //Console.WriteLine("Værdierne er sendt");
            //Console.WriteLine("Tryk for test af SendDTO_Raw");
           

           




            //Dette er test af HW integration
            //Console.WriteLine("Test af Measure: differential");
            //var measuredVal = _adc.Measure();
            //Console.WriteLine("Den målte værdi er: " + measuredVal);
            //Console.ReadLine();
            //Console.WriteLine("Test af Battery: Single");
            //var batteryVal = _adc.MeasureBattery();
            //Console.WriteLine("Batteri i Volt er: " + batteryVal);
            //Console.ReadLine();
            //Console.WriteLine("Test af DoCalibration: Single");
            //var calVals = _adc.MeasureCalibration();
            //foreach (var VARIABLE in calVals)
            //{
            //    Console.WriteLine("Kalibreringsværdi: " + VARIABLE);
            //}

            //Console.ReadLine();
            //Console.WriteLine("Nu burde LED'en lyse");
            //indicateBattery.IndicateLowBattery();
            //Console.WriteLine("Tryk på en tast for at slukke");
            //Console.ReadLine();
            //indicateBattery.TurnOff();

            //Console.WriteLine("Test af højtaler");
            //Console.ReadLine();
            //alarm.StartHighAlarm();
            //Console.WriteLine("Tryk for at slukke igen");
            //Console.ReadLine();
            //alarm.StopHighAlarm();

        }


    }
}
