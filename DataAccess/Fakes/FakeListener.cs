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
    /// <summary>
    /// class som skal gøre det ud for listeneren når der testes
    /// </summary>
    public class FakeListener : IListener
    {
        /// <summary>
        /// propperty til den kommando der kommer ind i systemet
        /// </summary>
        public string Command { get; set; }
        /// <summary>
        /// propperty til den dto med grænseværdier, der kommer ind i systemet
        /// </summary>
        public DTO_LimitVals DtoLimit { get; set; }
        private int count;
        private int lCount;
        private bool _systemOn;
        private DataContainerUdp readingCommand;
        private DataContainerUdp readingLimit;
        private readonly BlockingCollection<DataContainerUdp> _dataQueueCommands;
        private readonly BlockingCollection<DataContainerUdp> _dataQueueLimit;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="dataQueueCommands">datakø til komandoer fra UI</param>
        /// <param name="dataQueueLimit">datakø til grænseværdier fra UI</param>
        public FakeListener(BlockingCollection<DataContainerUdp> dataQueueCommands, BlockingCollection<DataContainerUdp> dataQueueLimit)
        {
            readingCommand = new DataContainerUdp();
            readingLimit = new DataContainerUdp();
            _dataQueueCommands = dataQueueCommands;
            _dataQueueLimit = dataQueueLimit;
            
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
        /// metode, der lytter efter komandoer
        /// </summary>
        public void ListenCommandsPC()
        {
            while (_systemOn)
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

        /// <summary>
        /// metode, der lytter efter grænseværdier
        /// </summary>
        public void ListenLimitValsPC()
        {
           
                if (lCount != 1)
                {
                    DtoLimit = new DTO_LimitVals(120, 80, 90, 20, 120, 20, 1, 2);
                    lCount++;
                    AddToQueueDtoLimitVals(DtoLimit);
                }
            

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