using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using DTO_s;

using System.Text.Json;
using System.Text.Json.Serialization;
using BusinessLogic;

namespace PresentationLogic
{
    public class UdpListener
    {
        
        private const int listenPort = 11000;
        private const int listenPortCommand = 12000;
        private readonly PresentationController presentationConObj= new PresentationController();
        private readonly ZeroAdjustment zeroAdjustment= new ZeroAdjustment();
        public string Command { get; private set; }

        public void ListenCommands()
        {
            UdpClient listener= new UdpClient(listenPort);
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
                            presentationConObj.StartMonitoringRequest();
                            break;
                        case "Startzeroing":
                            presentationConObj.ZeroAdjustRequest();
                            break;
                        case "Startcalibration":
                            presentationConObj.CalibrationRequest();
                            break;
                        case "Mutealarm":
                            presentationConObj.MuteRequest();
                            break;
                        case "Stop":
                            presentationConObj.StopMonitoring();
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

        public void ListenCalibrationVal()
        {
            UdpClient listener= new UdpClient(listenPort);
            IPEndPoint endPoint= new IPEndPoint(IPAddress.Any, listenPort);
            byte[] bytes;
            double calibrationVal;
            try
            {
                while (true)
                {
                    bytes = listener.Receive(ref endPoint);
                    calibrationVal = Convert.ToDouble(Encoding.ASCII.GetString(bytes, 0, bytes.Length));
                    presentationConObj.CalibrationVal(calibrationVal);
                }
            }
            catch (Exception e)
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
            UdpClient listener= new UdpClient(listenPort);
            IPEndPoint endPoint=new IPEndPoint(IPAddress.Broadcast, listenPort);
            DTO_LimitVals limitVals; 

            try
            {
                while (true)
                {
                    byte[] bytes = listener.Receive(ref endPoint);
                    string jsonString = Encoding.ASCII.GetString(bytes,0,bytes.Length);
                    limitVals = JsonSerializer.Deserialize<DTO_LimitVals>(jsonString);
                    presentationConObj.LimitValsEntered(limitVals);
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

        public void ListenZeroAdjustVal()
        {
            UdpClient listener = new UdpClient(listenPort);
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, listenPort);
            byte[] bytes;
            double zeroAdjustVal;
            try
            {
                while (true)
                {
                    bytes = listener.Receive(ref endPoint);
                    zeroAdjustVal = Convert.ToDouble(Encoding.ASCII.GetString(bytes, 0, bytes.Length));
                    zeroAdjustment.ZeroAdjustMean=zeroAdjustVal;
                }
            }
            catch (Exception e)
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
