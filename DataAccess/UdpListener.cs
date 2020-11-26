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
    public class UdpListener
    {
        
        public string Command { get; private set; }
       public DTO_LimitVals DtoLimit { get; private set; }
        
        
        public string ListenCommandsPC()
        {
            const int listenPort = 11000;
            UdpClient listener = new UdpClient(listenPort);
            IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, listenPort);
            try
            {
                while (true)
                {
                    byte[] bytes = listener.Receive(ref groupEP);
                    Command = Encoding.ASCII.GetString(bytes, 0,
                        bytes.Length); 

                    return Command;
                }
            }

            catch (SocketException e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                listener.Close();
            }
        }

        public DTO_LimitVals ListenLimitValsPC()
        {
            const int listenPort = 11003;
           UdpClient listener= new UdpClient(listenPort);
            IPEndPoint endPoint=new IPEndPoint(IPAddress.Broadcast, listenPort);
          
            try
            {
                while (true)
                {
                    byte[] bytes = listener.Receive(ref endPoint);
                    string jsonString = Encoding.ASCII.GetString(bytes,0,bytes.Length);
                    DtoLimit = JsonSerializer.Deserialize<DTO_LimitVals>(jsonString);
                    return DtoLimit;
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                listener.Close();
            }
        }

    }

}
