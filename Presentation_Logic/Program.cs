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

            BlockingCollection<DataContainerMeasureVals> dataQueueMeasure= new BlockingCollection<DataContainerMeasureVals>();
            BlockingCollection<DataContainerUdp> dataQueueCommand= new BlockingCollection<DataContainerUdp>();
            BlockingCollection<DataContainerUdp> dataQueueLimit= new BlockingCollection<DataContainerUdp>();
           
            BusinessController businessController= new BusinessController(dataQueueCommand,dataQueueLimit,dataQueueMeasure);

            PresentationController presentationController= new PresentationController(businessController);

             //presentationController.RunCommandsTest();
             Thread consumerCommands= new Thread(presentationController.RunConsumerCommands);
             Thread consumerLimit= new Thread(presentationController.RunConsumerLimit);
             Thread listenCommands = new Thread(presentationController.CheckCommands); // Disse skal muligvis kaldes i en metode
            Thread listenLimitVal= new Thread(presentationController.CheckLimit); // Disse skal muligvis kaldes i en metode 
            Thread producerCommands= new Thread(presentationController.RunProducerCommands);
            Thread producerLimits= new Thread(presentationController.RunProducerLimit);
            

            producerLimits.Start();
            producerCommands.Start();
            consumerCommands.Start();
            consumerLimit.Start();
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
