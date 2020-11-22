using System.Net;
using System.Net.Sockets;
using System.Text;

namespace DataAccessLogic
{
    public class UdpSender
    {
        private const int listenPort = 11000;
        private const int listenPortCommand = 12000;
        IPAddress ipAddress = IPAddress.Parse("JegKenderIkkeIPAdresse");

        public void SendZeroAdjustMean(double zeroAdjustMean)
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            IPEndPoint endPoint= new IPEndPoint(ipAddress,listenPortCommand);

            double _zeroAdjustMean = zeroAdjustMean;
            while (true)
            {
                byte[] sendbuf = Encoding.ASCII.GetBytes(_zeroAdjustMean.ToString());
                socket.SendTo(sendbuf,endPoint);
            }
        }

        public void SendCalibration(double calibrationVal)
        {

        }

    }
}