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


        static void Main(string[] args)
        {
         
            Console.WriteLine("Hello World!");

            BlockingCollection<DataContainerMeasureVals> dataQueueMeasure = new BlockingCollection<DataContainerMeasureVals>();
            BlockingCollection<DataContainerUdp> dataQueueCommand = new BlockingCollection<DataContainerUdp>();
            BlockingCollection<DataContainerUdp> dataQueueLimit = new BlockingCollection<DataContainerUdp>();

            BusinessController businessController = new BusinessController(dataQueueCommand, dataQueueLimit, dataQueueMeasure);

            PresentationController presentationController = new PresentationController(businessController);
         

            Thread consumerCommands = new Thread(presentationController.RunConsumerCommands);
            Thread consumerLimit = new Thread(presentationController.RunConsumerLimit);
           
            Thread producerCommands = new Thread(presentationController.RunProducerCommands);
            Thread producerLimits = new Thread(presentationController.RunProducerLimit);


            producerLimits.Start();
            producerCommands.Start();
            consumerCommands.Start();
            consumerLimit.Start();
        

       







        }


    }
}
