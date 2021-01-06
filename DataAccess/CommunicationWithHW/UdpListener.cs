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
    /// <summary>
    /// class der modtager informationer til UI over UDP
    /// </summary>
    public class UdpListener : IListener
    {
        /// <summary>
        /// propperty til den kommando der kommer ind i systemet
        /// </summary>
        public string Command { get; set; }
        /// <summary>
        /// propperty til den dto med grænseværdier, der kommer ind i systemet
        /// </summary>
        public DTO_LimitVals DtoLimit = new DTO_LimitVals(0,0,0,0,0,0,0,0);

        //public DTO_LimitVals DtoLimit { get; set; }
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
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="dataQueueCommands">datakø til komandoer fra UI</param>
        /// <param name="dataQueueLimit">datakø til grænseværdier fra UI</param>
        public UdpListener(BlockingCollection<DataContainerUdp> dataQueueCommands, BlockingCollection<DataContainerUdp> dataQueueLimit)
        {
            _dataQueueCommands = dataQueueCommands;
            _dataQueueLimit = dataQueueLimit;
            readingCommand = new DataContainerUdp();
            readingLimit = new DataContainerUdp();
        }
        /// <summary>
        /// bool der indikere om systemet er tændt
        /// </summary>
        /// <param name="systemOn">bool der indikere om systemet er tændt</param>
        public void ReceiveSystemOn(bool systemOn)
        {
            _systemOn = systemOn;

        }
        /// <summary>
        /// metode, der lytter efter komandoer
        /// </summary>
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

        /// <summary>
        /// metode, der lytter efter grænseværdier
        /// </summary>
        public void ListenLimitValsPC()
        {
            const int listenPortLimit = 11004;
            listenerLimit = new UdpClient(listenPortLimit);
            endPointLimit = new IPEndPoint(IpAddress, listenPortLimit);
            Console.WriteLine("udolistener listenlimitvals");
            try
            {
                Console.WriteLine("i tryen");
                while (_systemOn)
                {

                    Console.WriteLine("i whilen");
                    byte[] bytes = listenerLimit.Receive(ref endPointLimit);
                    string jsonString = Encoding.ASCII.GetString(bytes, 0, bytes.Length);
                    Console.WriteLine(jsonString);
                    DtoLimit = JsonSerializer.Deserialize<DTO_LimitVals>(jsonString);
                    Console.WriteLine(""+DtoLimit);
                    Console.WriteLine("i whilen");
                    //SendDtoLimitVals(DtoLimit);
                    if (DtoLimit != null)
                    {
                        AddToQueueDtoLimitVals(DtoLimit);
                    }
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

        //public DTO_LimitVals ListenLimitValsPC()
        //{
        //    const int listenPortLimit = 11004;
        //    listenerLimit = new UdpClient(listenPortLimit);
        //    endPointLimit = new IPEndPoint(IpAddress, listenPortLimit);

        //    try
        //    {
        //        while (_systemOn)

        //        {
        //            Console.WriteLine("whileloop 1");
        //            byte[] bytes = listenerLimit.Receive(ref endPointLimit);
        //            Console.WriteLine("whileloop 2");
        //            string jsonString = Encoding.ASCII.GetString(bytes, 0, bytes.Length);
        //            Console.WriteLine(jsonString);
        //            DtoLimit = JsonSerializer.Deserialize<DTO_LimitVals>(jsonString);
        //            Console.WriteLine(DtoLimit);
        //            return DtoLimit;
        //            //AddToQueueDtoLimitVals(DtoLimit); 
        //        }
        //        _dataQueueLimit.CompleteAdding();
        //    }
        //    catch (SocketException e)
        //    {
        //        Console.WriteLine(e);
        //        throw;
        //    }
        //    finally
        //    {
        //        listenerLimit.Close();
        //    }

        //    return null;
        //}


            public DTO_LimitVals SendDtoLimitVals(DTO_LimitVals dtoLimit)
        {
            return dtoLimit;
        }
        /// <summary>
        /// metode, der tilføjer komandoen til datakøen for komandoer
        /// </summary>
        /// <param name="command">komandoen fra UI, der sætter RPi igang</param>
        public void AddToQueueCommand(string command)
        {
            readingCommand.SetCommand(command);
            _dataQueueCommands.Add(readingCommand);
            Thread.Sleep(10);
        }
        /// <summary>
        /// metode, der tilføjer limitvals til datakøen for komandoer
        /// </summary>
        /// <param name="dtoLimit">dto bestående af øvre og nedre grænse for sys, dia, middel blodtryk samt nulpunktjustering og calkibrationsværdi</param>
        public void AddToQueueDtoLimitVals(DTO_LimitVals dtoLimit)
        {
            readingLimit.SetLimitVals(dtoLimit);
            _dataQueueLimit.Add(readingLimit);
            Thread.Sleep(10);
        }
    }
}
