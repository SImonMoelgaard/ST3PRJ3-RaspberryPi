using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using RaspberryPiCore;
using DTO_s;


namespace DataAccessLogic
{
    /// <summary>
    /// class til kommunikation med højtaleren
    /// </summary>
    public class Alarm : IAlarm
    {
        
        string _highAlarm = "cardiHighAlarm.wav";
        string _mediumAlarm = "cardiMedAlarm.wav";
        private System.Diagnostics.Process _highStart;
        private System.Diagnostics.Process _mediumStart;
        private bool _highOn;
        private bool _mediumOn;


        /// <summary>
        /// Muter alarmen i 5 minutter, hvis der bliver trykket på en knap på UI
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
        /// <summary>
        /// stopper alarm med høj prioritet
        /// </summary>
        public void StopHighAlarm()
        {
            _highStart.Kill();
        }
        /// <summary>
        /// stopper alarm med mellem prioritet
        /// </summary>
        public void StopMediumAlarm()
        {
            _mediumStart.Kill();
        }
        /// <summary>
        /// starter alarm med høj prioritet
        /// </summary>
        public void StartHighAlarm()
        {
            while (true)
            {
                _highStart = System.Diagnostics.Process.Start("cvlc", $"--no-video {_highAlarm}");
                _highOn = true;
            }

        }
        /// <summary>
        /// starter alarm med mellem prioritet
        /// </summary>
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