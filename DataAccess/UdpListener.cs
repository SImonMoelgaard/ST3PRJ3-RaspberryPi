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
        private IPEndPoint endPointCommand;
        private UdpClient listenerLimit;
        private IPEndPoint endPointLimit;
        private Producer producer;
        private readonly BlockingCollection<DataContainerUdp> _dataQueueCommands;
        private DataContainerUdp readingCommand;
        public UdpListener(BlockingCollection<DataContainerUdp> dataQueueCommands)
        {
            _dataQueueCommands = dataQueueCommands;
            readingCommand = new DataContainerUdp();
        }

        public void ListenCommandsPC()
        {
            const int listenPortCommand = 11000;
            listenerCommand = new UdpClient(listenPortCommand);
            endPointCommand = new IPEndPoint(IpAddress, listenPortCommand);


            try
            {
                while (true) //systenOn 
                {
                    byte[] bytes = listenerCommand.Receive(ref endPointCommand);
                    Command = Encoding.ASCII.GetString(bytes, 0,
                        bytes.Length);
                    Console.WriteLine(Command);
                    SendCommand(Command);
                    //return Command; //overvej hvor return skal være henne 


                }
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

        public void SendCommand(string command)
        {
            //while (true/*_systemOn*/)
            // {

            //_udpListener.ListenCommandsPC();
            //string command = _udpListener.Command;
            Console.WriteLine("listener sendcommand" + command);
            readingCommand.SetCommand(command);
            Console.WriteLine(" " + command);
            _dataQueueCommands.Add(readingCommand);
            Thread.Sleep(10);

            // }
            //   _dataQueueCommands.CompleteAdding();
        }

        public void ListenLimitValsPC()
        {
            const int listenPortLimit = 11004;
            listenerLimit = new UdpClient(listenPortLimit);
            endPointLimit = new IPEndPoint(IpAddress, listenPortLimit);

            try
            {
                while (true) //systenOn
                             //udp sendes i små pakker, men vi returnere med det samme. evt overvej while løkken ikke er evig, men sat til indtil udp er færdig(evt med et !! til sidst) og så først returnere efter while løkken
                {
                    byte[] bytes = listenerLimit.Receive(ref endPointLimit);
                    string jsonString = Encoding.ASCII.GetString(bytes, 0, bytes.Length);
                    DtoLimit = JsonSerializer.Deserialize<DTO_LimitVals>(jsonString);
                    //Console.WriteLine("listener" + Command);
                    if (DtoLimit.CalVal == 0)
                    {
                        DtoLimit.CalVal = 0.00048828;
                    } //bare til at køre med

                    SendDtoLimitVals(DtoLimit); //overvej hvor vi returner 
                }
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

        public DTO_LimitVals SendDtoLimitVals(DTO_LimitVals dtoLimit)
        {
            return dtoLimit;
        }

    }
}
