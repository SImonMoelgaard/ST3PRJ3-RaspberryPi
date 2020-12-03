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
        // public bool AlarmOn { get; set; }
        string _highAlarm = "high.mp3";
        string _lowAlarm = "medium.mp3";
        //private readonly int _sleepTime = 300000;
        private System.Diagnostics.Process _highStart;
        private System.Diagnostics.Process _mediumStart;



        //}
        /// <summary>
        /// Muter alarmen i x minutter, hvis der bliver trykket på en knap
        /// </summary>
        public void Mute()
        {
            
        }

        public void StopHighAlarm()
        {
            _highStart.Kill();
        }

        public void StopMediumAlarm()
        {
            _mediumStart.Kill();
        }

        public void StartHighAlarm()
        {
            while (true)
            {
                _highStart = System.Diagnostics.Process.Start("cvlc", $"--no-video {_highAlarm}");
            }
            
        }

        public void StartMediumAlarm()
        {
            while (true)
            {
                _mediumStart = System.Diagnostics.Process.Start("cvlc", $"--no-video {_lowAlarm}");
            }
        }
    }
}
