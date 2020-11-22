using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using DTO_s;

using System.Text.Json;
using System.Text.Json.Serialization;

namespace PresentationLogic
{
    public class UdpListener
    {
        private ReceiveUI receiveUi= new ReceiveUI();
        private const int listenPort = 11000;
        private const int listenPortCommand = 12000;
        public string Command { get; private set; }

        public void ListenCommands()
        {
            UdpClient listener= new UdpClient(listenPortCommand);
            IPEndPoint groupEP= new IPEndPoint(IPAddress.Any, listenPort);
            try
            {
                while (true)
                {
                    byte[] bytes = listener.Receive(ref groupEP);
                    Command = Encoding.ASCII.GetString(bytes, 0,
                        bytes.Length); //hvorfor skal der står bytes, 0, bytes.Length?? hvorfor er det ikke nok med bytes
                    switch (Command)
                    {
                        case "Startmeasurment":
                            receiveUi.StartRequest();
                            break;
                        case "Startzeroing":
                            receiveUi.ZeroAdjustmentRequest();
                            break;
                        case "Startcalibration":
                            receiveUi.UICalibrationRequest();
                            break;
                        case "New limit vals":
                            receiveUi.ReceiveLimitVals();
                            break;
                        case "Mutealarm":
                            receiveUi.ReceiveMute();
                            break;
                        case "Stop":
                            receiveUi.StopMonitoringRequest();
                            break;
                    }

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

        public void ListenDTO()
        {
            UdpClient listener= new UdpClient(listenPort);
            IPEndPoint endPoint=new IPEndPoint(IPAddress.Broadcast, listenPort);
            DTO_Bloodpreassure command;

            try
            {
                while (true)
                {
                    byte[] bytes = listener.Receive(ref endPoint);
                    string jsonString = Encoding.ASCII.GetString(bytes,0,bytes.Length);
                    command = JsonSerializer.Deserialize<DTO_Bloodpreassure>(jsonString);
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                listener.Close();
            }
        }
    }
}
