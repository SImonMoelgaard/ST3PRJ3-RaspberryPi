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
                        bytes.Length); //hvorfor skal der står bytes, 0, bytes.Length?? hvorfor er det ikke nok med bytes

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

        public void ListenLimitVals()
        {
            const int listenPort = 11003;
           UdpClient listener= new UdpClient(listenPort);
            IPEndPoint endPoint=new IPEndPoint(IPAddress.Broadcast, listenPort);
            DTO_LimitVals limitVals;
            double zeroVal;
            double calVal;

            try
            {
                while (true)
                {
                    byte[] bytes = listener.Receive(ref endPoint);
                    string jsonString = Encoding.ASCII.GetString(bytes,0,bytes.Length);
                    limitVals = JsonSerializer.Deserialize<DTO_LimitVals>(jsonString);
                    _presentationCon.LimitValsEntered(limitVals);
                    zeroVal = limitVals.ZeroVal;
                    calVal = limitVals.CalVal;
                    _presentationCon.ZeroValReceived(zeroVal);
                    _presentationCon.CalibrationVal(calVal);
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
