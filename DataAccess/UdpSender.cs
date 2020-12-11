using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using DTO_s;
using Newtonsoft.Json;
//using System.Text.Json;
//using System.Text.Json.Serialization;
//using JsonSerializer = System.Text.Json.JsonSerializer;

namespace DataAccessLogic
{
    public class UdpSender: ISender
    {

        private static readonly IPAddress IpAddress = IPAddress.Parse("172.20.10.6");

        public void SendDouble(double value)
        {
            const int listenPort = 11004;
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint endPoint= new IPEndPoint(IpAddress,listenPort);

            double _value = value;
            
            byte[] sendBuf = Encoding.ASCII.GetBytes(_value.ToString());
            socket.SendTo(sendBuf, endPoint);
            
        }

        public void SendDTO_Calculated(DTO_Calculated dtoCalculated)

        {
            const int listenPort = 11001;
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            IPEndPoint endPoint= new IPEndPoint(IpAddress, listenPort); 
            List<DTO_Calculated> dto = new List<DTO_Calculated>();
            dto.Add(dtoCalculated);
            var json = JsonConvert.SerializeObject(dto);
           
            byte[] sendBuf = Encoding.ASCII.GetBytes(json);
            socket.SendTo(sendBuf, endPoint);
            Console.WriteLine("Data Calculated er nu sendt");
            
        }

        public void SendDTO_Raw(List<DTO_Raw> dtoRaw)
        {
            const int listenPort = 11001;
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint endPoint = new IPEndPoint(IpAddress, listenPort);
            List<DTO_Raw> dto = dtoRaw;
            var json = JsonConvert.SerializeObject(dto);
           
            byte[] sendBuf = Encoding.ASCII.GetBytes(json);
            socket.SendTo(sendBuf, endPoint);
            Console.WriteLine("Data er nu sendt");

        }

      
    }
}