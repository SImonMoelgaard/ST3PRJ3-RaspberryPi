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
    public class UdpListener : IListener
    {
        
        public string Command { get; private set; }
       public DTO_LimitVals DtoLimit { get; private set; }
       private static readonly IPAddress IpAddress = IPAddress.Parse("172.20.10.3");


        public string ListenCommandsPC()
        {
            const int listenPort = 11000;
            UdpClient listener = new UdpClient(listenPort);
            IPEndPoint groupEP = new IPEndPoint(IpAddress, listenPort);
            try
            {
                while (true) //systenOn 
                {
                    byte[] bytes = listener.Receive(ref groupEP);
                    Command = Encoding.ASCII.GetString(bytes, 0,
                        bytes.Length);
                    return Command; //overvej hvor return skal være henne 

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
            IPEndPoint endPoint=new IPEndPoint(IpAddress, listenPort);
          
            try
            {
                while (true) //systenOn
                    //udp sendes i små pakker, men vi returnere med det samme. evt overvej while løkken ikke er evig, men sat til indtil udp er færdig(evt med et !! til sidst) og så først returnere efter while løkken
                {
                    byte[] bytes = listener.Receive(ref endPoint);
                    string jsonString = Encoding.ASCII.GetString(bytes,0,bytes.Length);
                    DtoLimit = JsonSerializer.Deserialize<DTO_LimitVals>(jsonString);
                    return DtoLimit; //overvej hvor vi returner 
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
