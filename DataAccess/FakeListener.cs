using System;
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
    public class FakeListener : IListener
    {
        
        public string Command { get; private set; }
       public DTO_LimitVals DtoLimit { get; set; }
       private int count = 0;
       private int lCount = 0;
        
        
        public string ListenCommandsPC()
        {
            if (count != 1)
            {
                Command = "Startmeasurment";
                count=1;
            }
            //else Command = null;

            return Command;
        }

        public DTO_LimitVals ListenLimitValsPC()
        {
            
            if (lCount != 1)
            {
                DtoLimit = new DTO_LimitVals(120, 80, 90, 20, 60, 20, 1, 2);
                lCount=1;
            }
            //DtoLimit.HighSys = 120;
            //DtoLimit.LowSys = 80;
            //DtoLimit.HighDia = 90;
            //DtoLimit.LowDia = 20;
            //DtoLimit.HighMean = 80;
            //DtoLimit.LowMean = 60;
            //DtoLimit.ZeroVal = 2;
            //DtoLimit.CalVal = 1;
            return DtoLimit;
        }

    }

}
