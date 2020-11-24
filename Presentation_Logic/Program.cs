using System;
using System.Threading;
using RaspberryPiCore.ADC;
using RaspberryPiCore.TWIST;
using RaspberryPiCore.LCD;
using PresentationLogic;

namespace Raspberry_Pi_Dot_Net_Core_Console_Application3
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            PresentationController presentationController= new PresentationController();
            
            UdpListener listener= new UdpListener();

            Thread listenCommands= new Thread(listener.ListenCommands);
            Thread listenLimitVals= new Thread(listener.ListenLimitVals);

            listenCommands.Start();
            listenLimitVals.Start();





        }
    }
}
