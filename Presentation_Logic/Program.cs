using System;
using System.Collections.Concurrent;
using System.Threading;
using BusinessLogic;
using RaspberryPiCore.ADC;
using RaspberryPiCore.TWIST;
using RaspberryPiCore.LCD;
using BusinessLogic;
using DataAccessLogic;
using RPI;
using Led = RaspberryPi.Led;


namespace BP_program
{
    class Program 
    {
        //private static RaspberryPiCore rpi;
        private static RaspberryPi.RaspberryPiDll rpi;
        
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            BlockingCollection<DataContainerMeasureVals> measureContainer= new BlockingCollection<DataContainerMeasureVals>();
            BlockingCollection<DataContainerUdp> udpContainer= new BlockingCollection<DataContainerUdp>();
           
            BusinessController businessController= new BusinessController(udpContainer,measureContainer);
            PresentationController presentationController= new PresentationController(businessController);
            
            
            Thread listenCommands= new Thread(presentationController.RunCommands);
            Thread listenLimitVal= new Thread(presentationController.RunLimit);

            listenCommands.Start();
            listenLimitVal.Start();


            

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

     
    }
}
