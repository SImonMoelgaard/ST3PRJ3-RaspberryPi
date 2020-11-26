using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Text;
using System.Threading;

namespace DataAccessLogic
{
    public class ProducerCommand
    {
        // Denne klasse skal måske slettes 
        private readonly BlockingCollection<DataContainerUdp> dataQueue;
        private readonly UdpListener udpListener = new UdpListener();

        public ProducerCommand(BlockingCollection<DataContainerUdp> dataQueue)
        {
            this.dataQueue = dataQueue;
        }

        public void Run(bool systemOn)
        {
            while (systemOn) //Denne bool skal sættes til true når programmet starter op, og sættes til false, når programmet lukkes ned
            {
                DataContainerUdp reading = new DataContainerUdp();
                var command = udpListener.ListenCommandsPC();
                reading.SetCommand(command);
                dataQueue.Add(reading);
                Thread.Sleep(10);
                
            }
            dataQueue.CompleteAdding();
        }

    }
}
