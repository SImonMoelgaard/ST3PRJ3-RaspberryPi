using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using DTO_s;
using Newtonsoft.Json.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;


namespace DataAccessLogic
{
    public class UdpListener : IListener
    {

        public string Command { get; set; }
        public DTO_LimitVals DtoLimit { get; set; }
        private static readonly IPAddress IpAddress = IPAddress.Parse("172.20.10.6");
        private UdpClient listenerCommand;
        private UdpClient listenerLimit;
        private IPEndPoint endPointCommand;
        private IPEndPoint endPointLimit;
        private readonly BlockingCollection<DataContainerUdp> _dataQueueCommands;
        private readonly BlockingCollection<DataContainerUdp> _dataQueueLimit;
        private DataContainerUdp readingCommand;
        private DataContainerUdp readingLimit;
        private bool _systemOn;
        public UdpListener(BlockingCollection<DataContainerUdp> dataQueueCommands, BlockingCollection<DataContainerUdp> dataQueueLimit)
        {
            _dataQueueCommands = dataQueueCommands;
            _dataQueueLimit = dataQueueLimit;
            readingCommand = new DataContainerUdp();
        }
        public void ReceiveSystemOn(bool systemOn)
        {
            _systemOn = systemOn;

        }
        public void ListenCommandsPC()
        {
            const int listenPortCommand = 11000;
            listenerCommand = new UdpClient(listenPortCommand);
            endPointCommand = new IPEndPoint(IpAddress, listenPortCommand);

            try
            {
                while (_systemOn)  
                {
                    byte[] bytes = listenerCommand.Receive(ref endPointCommand);
                    Command = Encoding.ASCII.GetString(bytes, 0,
                        bytes.Length);
                    Console.WriteLine(Command);
                    AddToQueueCommand(Command);
                }
                _dataQueueCommands.CompleteAdding();
            }

            catch (SocketException e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                listenerCommand.Close();
            }

        }

        public void ListenLimitValsPC()
        {
            const int listenPortLimit = 11004;
            listenerLimit = new UdpClient(listenPortLimit);
            endPointLimit = new IPEndPoint(IpAddress, listenPortLimit);

            try
            {
                while (_systemOn) 
                             
                {
                    byte[] bytes = listenerLimit.Receive(ref endPointLimit);
                    string jsonString = Encoding.ASCII.GetString(bytes, 0, bytes.Length);
                    DtoLimit = JsonSerializer.Deserialize<DTO_LimitVals>(jsonString);
                    AddToQueueDtoLimitVals(DtoLimit); 
                }
                _dataQueueLimit.CompleteAdding();
            }
            catch (SocketException e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                listenerLimit.Close();
            }
        }
        public void AddToQueueCommand(string command)
        {
            readingCommand.SetCommand(command);
            _dataQueueCommands.Add(readingCommand);
            Thread.Sleep(10);
        }
        public void AddToQueueDtoLimitVals(DTO_LimitVals dtoLimit)
        {
            readingLimit.SetLimitVals(dtoLimit);
            _dataQueueLimit.Add(readingLimit);
            Thread.Sleep(10);
        }
    }
}
