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
        private const int listenPort = 11000;
        private const int listenPortCommand = 12000;
        IPAddress ipAddress = IPAddress.Parse("JegKenderIkkeIPAdresse");

        public void SendDouble(double value)
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            IPEndPoint endPoint= new IPEndPoint(ipAddress,listenPortCommand);

            double _value = value;
            while (true)
            {
                byte[] sendbuf = Encoding.ASCII.GetBytes(_value.ToString());
                socket.SendTo(sendbuf,endPoint);
            }
        }

        public void SendDTO_Calculated(DTO_Calculated dtoCalculated)
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            IPEndPoint endPoint= new IPEndPoint(ipAddress, listenPortCommand);
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
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            IPEndPoint endPoint = new IPEndPoint(ipAddress, listenPortCommand);
            DTO_Raw dto = dtoRaw;
            var json = JsonConvert.SerializeObject(dto);
            while (true)
            {
                byte[] sendBuf = Encoding.ASCII.GetBytes(json);
                socket.SendTo(sendBuf, endPoint);
            }
        }

        public void SendDTO_ExceededVals(DTO_exceedVals dtoExceeded)
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            IPEndPoint endPoint = new IPEndPoint(ipAddress, listenPortCommand);
            DTO_exceedVals dto = dtoExceeded;
            var json = JsonConvert.SerializeObject(dto);
            while (true)
            {
                byte[] sendBuf = Encoding.ASCII.GetBytes(json);
                socket.SendTo(sendBuf, endPoint);
            }
        }
    }
}