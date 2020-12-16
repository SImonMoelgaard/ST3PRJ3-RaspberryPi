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
        /// Muter alarmen i 5 minutter, hvis der bliver trykket på en knap fra UI
        /// </summary>
        public void Mute()
        {
            Console.WriteLine("alarm off");
            Thread.Sleep(_sleepTime);
            Console.WriteLine("alarm on");
            //ved ikke lige hvordan man 
        }
        /// <summary>
        /// stopper alarm med høj prioritet
        /// </summary>
        public void StopHighAlarm()
        {
            Console.WriteLine("Alarm high off");

        }
        /// <summary>
        /// stopper alarm med mellem prioritet
        /// </summary>
        public void StopMediumAlarm()
        {
            Console.WriteLine("Alarm medium off");
        }
        /// <summary>
        /// starter alarm med høj prioritet
        /// </summary>
        public void StartHighAlarm()
        {
            Console.WriteLine("alarm high on");
        }
        /// <summary>
        /// starter alarm med mellem prioritet
        /// </summary>
        public void StartMediumAlarm()
        {
            Console.WriteLine("alarm medium on");
        }
    }
}