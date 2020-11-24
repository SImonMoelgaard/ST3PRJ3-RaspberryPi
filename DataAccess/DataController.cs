using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RaspberryPiCore;
using DTO_s;

namespace DataAccessLogic
{
    public class DataController
    {
       
        private readonly UdpSender _udpSender= new UdpSender();
        private readonly Alarm _alarm= new Alarm();

        public void SendMeanCal(double meanVal)
        {
            _udpSender.SendDouble(meanVal);
        }

       
        public void SendZero(double zeroAdjustMean)
        {
            _udpSender.SendDouble(zeroAdjustMean);
        }

        public void SendRaw(DTO_Raw raw)
        {
            _udpSender.SendDTO_Raw(raw);
        }

        public void SendDTOCalcualted(DTO_Calculated dtoCalculated)
        {
            _udpSender.SendDTO_Calculated(dtoCalculated);
        }

       

        public void AlarmRequestStart(string alarmType)
        {
            if (alarmType == "highSys")
            {
                _alarm.StartMediumAlarm();
            }

            if (alarmType == "lowMean")
            {
                _alarm.StartHighAlarm();
            }
        }

        public void MuteAlarm()
        {
            _alarm.Mute();
        }

        public void StopAlarm(string alarmType)
        {
            if (alarmType == "highSys")
            {
                _alarm.StopMediumAlarm();
            }

            if (alarmType == "lowMean")
            {
                _alarm.StopHighAlarm();
            }
        }
    }
}
