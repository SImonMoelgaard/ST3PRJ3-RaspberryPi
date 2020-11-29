using System;
using System.Collections.Concurrent;
using System.Threading;
using BusinessLogic;
using RaspberryPiCore.ADC;
using RaspberryPiCore.TWIST;
using RaspberryPiCore.LCD;
using BusinessLogic;
using RPI;
using Led = RaspberryPi.Led;


namespace BP_program
{
    class Program : IPresentationObserver
    {
        //private static RaspberryPiCore rpi;
        private static RaspberryPi.RaspberryPiDll rpi;
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");


            PresentationController presentationController= new PresentationController();
            
            



            //UdpListener listener= new UdpListener();

            //Thread listenCommands= new Thread(listener.ListenCommands);
            //Thread listenLimitVals= new Thread(listener.ListenLimitVals);

            //listenCommands.Start();
            //listenLimitVals.Start();

            // BlockingCollection<DataContainerUdp> queue = new BlockingCollection<DataContainerUdp>();
            //ProducerCommand producerCommandListener= new ProducerCommand(queue);
            //ConsumerUdp consumerUdp= new ConsumerUdp(queue);

            //Thread UdpListenerCommandT = new Thread(producerCommandListener.Run);
            //Thread consumerUdpT= new Thread(consumerUdp.Run);

            //       UdpListenerCommandT.Start();
            //     consumerUdpT.Start();






        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
