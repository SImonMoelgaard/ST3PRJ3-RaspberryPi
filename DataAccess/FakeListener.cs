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
    public class FakeListener
    {
        
        public string Command { get; private set; }
       public DTO_LimitVals DtoLimit { get; private set; }
        
        
        public string ListenCommandsPC()
        {
            return "Startmeasurment";
        }

        public DTO_LimitVals ListenLimitValsPC()
        {
            return new DTO_LimitVals(120,80,90,20,60,70,0,0);
        }

    }

}
