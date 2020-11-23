using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using RaspberryPiCore;
using DTO_s;


namespace DataAccessLogic
{
    public class Alarm
    {
        public bool AlarmOn { get; set; }

        /// <summary>
        /// Starter alarmen (!!!!!! LED, højtaler eller hvad??) 
        /// </summary>
        // Starter alarmen 
        public void StartAlarm() // vi skal lige finde ud af hvordan vi skelner mellem hvilken alarm der startes (hvis vi har 2- medium og high)
        {
            if (AlarmOn == false)
            {
                Console.WriteLine("alarm on");
                AlarmOn = true;
            }
            
        }
        /// <summary>
        /// Muter alarmen i x minutter, hvis der bliver trykket på en knap
        /// </summary>
        public void Mute()
        {
            if (AlarmOn)
            {
                Console.WriteLine("alarm off");
                Thread.Sleep(18000);
                Console.WriteLine("alarm on");
                //ved ikke lige hvordan man 
            }
        }

        public void StopAlarm()
        {
            if (AlarmOn)
            {
                Console.WriteLine("Alarm on");
                AlarmOn = false;
            }
            
        }

    }
}
