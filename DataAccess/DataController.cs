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
       
        private readonly UdpSender udpSender= new UdpSender();
        

        
        private readonly Alarm alarm= new Alarm();

        public void SendMeanCal(double meanVal)
        {
            udpSender.SendDouble(meanVal);
        }

       
        public void SendZero(double zeroAdjustMean)
        {
            udpSender.SendDouble(zeroAdjustMean);
        }

        public void SendRaw(DTO_Raw raw)
        {
            udpSender.SendDTO_Raw(raw);
        }

        public void SendDTOCalcualted(DTO_Calculated DtoCalculated)
        {
            udpSender.SendDTO_Calculated(DtoCalculated);
        }

        public void SendExceededVals(DTO_exceedVals exceededVals)
        {
            udpSender.SendDTO_ExceededVals(exceededVals);
        }

        public void AlarmRequestStart(string alarmType)
        {
            if (alarmType == "highSys")
            {
                alarm.StartMediumAlarm();
            }

            if (alarmType == "lowMean")
            {
                alarm.StartHighAlarm();
            }
        }

        public void MuteAlarm()
        {
            alarm.Mute();
        }

        public void StopAlarm(string alarmType)
        {
            if (alarmType == "highSys")
            {
                alarm.StopMediumAlarm();
            }

            if (alarmType == "lowMean")
            {
                alarm.StopHighAlarm();
            }
        }
    }
}
