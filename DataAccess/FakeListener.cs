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
        
        public string Command { get; private set; }
       public DTO_LimitVals DtoLimit { get; private set; }
       private int count;
       private int lCount;
        
        
        public string ListenCommandsPC()
        {
            while (true)
            {
                if (count != 1)
                {
                    // Command = "Startmeasurment";

                    Command = "Startzeroing";

                    // Command ="Startcalibration";

                    // Command ="Mutealarm";

                    // Command ="Stop";

                    // Command = "SystemOff";
                    count = 1;
                }
                else
                {
                    Command = null;
                }
                return Command;
            }
            
        }

        public DTO_LimitVals ListenLimitValsPC()
        {
            if (lCount != 1)
            {
                DtoLimit= new DTO_LimitVals(120, 80, 90, 20, 60, 70, 1, 2);
            }

            return DtoLimit;
        }

    }

}
