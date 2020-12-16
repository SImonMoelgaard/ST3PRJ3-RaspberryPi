using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using RaspberryPiCore;
using DTO_s;


namespace DataAccessLogic
{
    public class FakeAlarm : IAlarm
    {
        // public bool AlarmOn { get; set; }
        private readonly int _sleepTime = 300000;



        //}
        /// <summary>
        /// Muter alarmen i x minutter, hvis der bliver trykket på en knap
        /// </summary>
        public void Mute()
        {
            Console.WriteLine("alarm off");
            Thread.Sleep(_sleepTime);
            Console.WriteLine("alarm on");
            //ved ikke lige hvordan man 
        }

        public void StopHighAlarm()
        {
            Console.WriteLine("Alarm high off");

        }

        public void StopMediumAlarm()
        {
            Console.WriteLine("Alarm medium off");
        }

        public void StartHighAlarm()
        {
            Console.WriteLine("alarm high on");
        }

        public void StartMediumAlarm()
        {
            Console.WriteLine("alarm medium on");
        }
    }
}