using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using RaspberryPiCore;
using DTO_s;


namespace DataAccessLogic
{
    public class Alarm : IAlarm
    {
        // public bool AlarmOn { get; set; }
        string _highAlarm = "cardiHighAlarm.wav";
        string _mediumAlarm = "cardiMedAlarm.wav";
        //private readonly int _sleepTime = 300000;
        private System.Diagnostics.Process _highStart;
        private System.Diagnostics.Process _mediumStart;
        private bool _highOn;
        private bool _mediumOn;



        //}
        /// <summary>
        /// Muter alarmen i x minutter, hvis der bliver trykket på en knap
        /// </summary>
        public void Mute()
        {
            if (_highOn)
            {
                _highStart.Kill();
                Thread.Sleep(300000);
            }
            if (_mediumOn)
            {
                _mediumStart.Kill();
                Thread.Sleep(300000);
            }

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
                _highOn = true;
            }

        }

        public void StartMediumAlarm()
        {
            while (true)
            {
                _mediumStart = System.Diagnostics.Process.Start("cvlc", $"--no-video {_mediumAlarm}");
                _mediumOn = true;
            }
        }
    }
}