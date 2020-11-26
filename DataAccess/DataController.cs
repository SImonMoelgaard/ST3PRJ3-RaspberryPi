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
        private readonly ReceiveAdc _adc= new ReceiveAdc();
        private List<double> calDoubles= new List<double>();
        

        public List<double> StartCal()
        {
           _adc.MeasureCalibration(calDoubles);
            return calDoubles;

        } 
        public List<double> StartZeroAdjust()
        {
            _adc.MeasureZeroAdjust(calDoubles);
            return calDoubles;

        }

        public double StartMeasure(bool startMonitoring)
        {
            while (startMonitoring)
            {
                return _adc.Measure();
            }
        }

        // Disse metoder skal ikke længere bruges tror jeg! 
        public void SendMeanCal(double meanVal)
        {
            _udpSender.SendDouble(meanVal);
        }

       
        public void SendZero(double zeroAdjustMean)
        {
            _udpSender.SendDouble(zeroAdjustMean);
        }

        public void SendRaw(List<DTO_Raw> _rawList)
        {
            List<double> _bpList = new List<double>();

           _udpSender.SendDTO_Raw(_rawList);
           foreach (var BP in _rawList)
           {
                _bpList.Add(BP.mmHg);
           }


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
