using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using DTO_s;
using Newtonsoft.Json.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Concurrent;
using System.Threading;

namespace DataAccessLogic
{
    public class FakeListener : IListener
    {

        public string Command { get; set; }
        public DTO_LimitVals DtoLimit { get; set; }
        private int count;
        private int lCount;
        private Producer producer;
        private bool _systemOn;
        private DataContainerUdp readingCommand;
        private DataContainerUdp readingLimit;
        private readonly BlockingCollection<DataContainerUdp> _dataQueueCommands;
        private readonly BlockingCollection<DataContainerUdp> _dataQueueLimit;


        public FakeListener(BlockingCollection<DataContainerUdp> dataQueueCommands, BlockingCollection<DataContainerUdp> dataQueueLimit)
        {
            readingCommand = new DataContainerUdp();
            readingLimit = new DataContainerUdp();
            _dataQueueCommands = dataQueueCommands;
            _dataQueueLimit = dataQueueLimit;
            
        }

        public void AddToQueueCommand(string command)
        {
            readingCommand.SetCommand(command);
            _dataQueueCommands.Add(readingCommand);
            Thread.Sleep(10);
        }

      

        public void ListenCommandsPC()
        {
            while (true)
            {
                if (count != 1)
                {
                    Command = "Startmeasurement";
                    // Command = "Startcalibration";
                    //Command = "Startzeroing";
                    AddToQueueCommand(Command);
                    count = 1;
                    Console.WriteLine(Command);
                }
                else
                {
                    Command = null;
                }

            }

        }
              

        public void ListenLimitValsPC()
        {
            if (lCount != 1)
            {
                DtoLimit = new DTO_LimitVals(120, 80, 90, 20, 120, 20, 1, 2);
                lCount++;
                AddToQueueDtoLimitVals(DtoLimit);
            }

            
        }

        public void ReceiveSystemOn(bool systemOn)
        {
            _systemOn = systemOn;
        }

        public void AddToQueueDtoLimitVals(DTO_LimitVals dtoLimit)
        {
            readingLimit.SetLimitVals(dtoLimit);
            _dataQueueLimit.Add(readingLimit);
            Thread.Sleep(10);
        }
    }

}