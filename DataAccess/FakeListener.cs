using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using DTO_s;
using Newtonsoft.Json.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;




namespace DataAccessLogic
{
    public class FakeListener : IListener
    {

        public string Command { get; set; }
        public DTO_LimitVals DtoLimit { get; set; }
        private int count;
        private int lCount;
        private Producer producer;




        public void SendCommand(string command)
        {
            producer.Command = command;
        }

        public DTO_LimitVals SendDtoLimitVals(DTO_LimitVals dtoLimit)
        {
            return dtoLimit;
        }

        public void ListenCommandsPC()
        {
            while (true)
            {
                if (Command != null)
                {
                    //Command = "Startmeasurement";
                    Command = "Startzeroing";
                    SendCommand(Command);
                    count = 1;
                    Console.WriteLine(Command);
                }
                else
                {
                    Command = null;
                }

                // SendCommand(Command);
            }

        }

        public void StartUp()
        {
            throw new NotImplementedException();
        }

        public void ListenLimitValsPC()
        {
            if (lCount != 1)
            {
                DtoLimit = new DTO_LimitVals(120, 80, 90, 20, 60, 70, 1, 2);
            }

            SendDtoLimitVals(DtoLimit);
        }

    }

}