using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Text;
using System.Threading;

namespace DataAccessLogic
{
    class ProducerUdp
    {
        private readonly BlockingCollection<DataContainerUdp> dataQueue;
        private readonly UdpListener udpListener = new UdpListener();

        public ProducerUdp(BlockingCollection<DataContainerUdp> dataQueue)
        {
            this.dataQueue = dataQueue;
        }

        public void Run()
        {
            int count = 0; //Find lige ud af hvor mange gange den counter skal køre.. den skal være der for at vi kan komme ud igen og sige complete adding
            // men jeg er lidt i tvivl om den så ligger 10 commands ind i min kø inden den siger complete adding.... 
            while (count<10)
            {
                DataContainerUdp reading = new DataContainerUdp();
                var command = udpListener.ListenCommandsPC();
                reading.SetCommand(command);
                dataQueue.Add(reading);
                Thread.Sleep(10);
                count++;

            }
            dataQueue.CompleteAdding();
        }

    }


}
