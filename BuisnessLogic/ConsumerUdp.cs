using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using DataAccessLogic;

namespace BusinessLogic
{
    public class ConsumerUdp
    {
        private readonly BlockingCollection<DataContainerUdp> dataQueue;
        

        public ConsumerUdp(BlockingCollection<DataContainerUdp> dataQueue)
        {
            this.dataQueue = dataQueue;
            
        }

        public void Run()
        {
            while (!dataQueue.IsCompleted)
            {
                try
                {
                    var container = dataQueue.Take(); // Tager et objekt ud af min kø når der er noget, ellers venter denn
                    var commandsPc = container.GetCommand();
                }
                catch (InvalidOperationException)
                {

                }
                Thread.Sleep(500);
            }
        }
    }

}
