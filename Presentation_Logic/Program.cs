using System;
using System.Collections.Concurrent;
using System.Threading;
using BusinessLogic;
using DataAccessLogic;
using RaspberryPiCore.ADC;
using RaspberryPiCore.TWIST;
using RaspberryPiCore.LCD;
using PresentationLogic;


namespace Raspberry_Pi_Dot_Net_Core_Console_Application3
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            //PresentationController presentationController= new PresentationController();

            //UdpListener listener= new UdpListener();

            //Thread listenCommands= new Thread(listener.ListenCommands);
            //Thread listenLimitVals= new Thread(listener.ListenLimitVals);

            //listenCommands.Start();
            //listenLimitVals.Start();

            BlockingCollection<DataContainerUdp> queue = new BlockingCollection<DataContainerUdp>();
            ProducerUdp producerUdpListener= new ProducerUdp(queue);
            ConsumerUdp consumerUdp= new ConsumerUdp(queue);

            Thread UdpListenerCommandT = new Thread(producerUdpListener.Run);
            Thread consumerUdpT= new Thread(consumerUdp.Run);

            UdpListenerCommandT.Start();
            consumerUdpT.Start();






        }
    }
}
