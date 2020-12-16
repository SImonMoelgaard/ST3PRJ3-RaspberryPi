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
    /// <summary>
    /// class der sender informationer til UI over UDP
    /// </summary>
    public class UdpSender : ISender
    {

        private static readonly IPAddress IpAddress = IPAddress.Parse("172.20.10.6");
        /// <summary>
        /// sender en double
        /// </summary>
        /// <param name="meanVal">enten zeroval eller en kalibreringsværdi</param>
        public void SendDouble(double value)
        {
            const int listenPort = 11004;
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint endPoint = new IPEndPoint(IpAddress, listenPort);

            double _value = value;
            byte[] sendBuf = Encoding.ASCII.GetBytes(_value.ToString());
            socket.SendTo(sendBuf, endPoint);
            Console.WriteLine("double :" + _value);
            socket.Close();

        }
        /// <summary>
        /// sender calculated bestående af sys, dia, middel, puls, batteristatus og om overskridelser af grænseværdier er sket
        /// </summary>
        /// <param name="dtoCalculated">dto calculated</param>
        public void SendDTO_Calculated(DTO_Calculated dtoCalculated)

        {
            const int listenPort = 11001;
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            IPEndPoint endPoint = new IPEndPoint(IpAddress, listenPort);
            List<DTO_Calculated> dto = new List<DTO_Calculated>();
            dto.Add(dtoCalculated);
            var json = JsonConvert.SerializeObject(dto);

            byte[] sendBuf = Encoding.ASCII.GetBytes(json);
            socket.SendTo(sendBuf, endPoint);
            Console.WriteLine("Data Calculated er nu sendt \r\n" + dtoCalculated.CalculatedSys);

        }
        /// <summary>
        /// sender liste med dto raw bestående af målinger over et halvt sekund i mmHg med dertilhørende tidspunkt
        /// </summary>
        /// <param name="dtoRaw"></param>
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