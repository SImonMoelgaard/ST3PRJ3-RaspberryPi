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
    public class UdpSender
    {

        private static readonly IPAddress IpAddress = IPAddress.Parse("172.20.10.3");

        public void SendDouble(double value)
        {
            const int listenPort = 11004;
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint endPoint= new IPEndPoint(IpAddress,listenPort);

            double _value = value;
            while (true)
            {
                byte[] sendBuf = Encoding.ASCII.GetBytes(_value.ToString());
                socket.SendTo(sendBuf, endPoint);
            }
        }

        public void SendDTO_Calculated(DTO_Calculated dtoCalculated)

        {
            const int listenPort = 11002;
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            IPEndPoint endPoint= new IPEndPoint(IpAddress, listenPort);
            DTO_Calculated dto = dtoCalculated;
            var json = JsonConvert.SerializeObject(dto);
            while (true)
            {
                byte[] sendBuf = Encoding.ASCII.GetBytes(json);
                socket.SendTo(sendBuf, endPoint);
            }
        }

        public void SendDTO_Raw(DTO_Raw dtoRaw)
        {
            const int listenPort = 11001;
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint endPoint = new IPEndPoint(IpAddress, listenPort);
            DTO_Raw dto = dtoRaw;
            var json = JsonConvert.SerializeObject(dto);
            while (true)
            {
                byte[] sendBuf = Encoding.ASCII.GetBytes(json);
                socket.SendTo(sendBuf, endPoint);
            }
        }

      
    }
}