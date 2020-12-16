using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using RaspberryPiCore;
using DTO_s;

namespace DataAccessLogic
{
    public class DataController
    {

        private readonly ISender _udpSender;
        private readonly IAlarm _alarm;
        private readonly IBPData _adc;
        private List<double> calDoubles;
        private bool _systemOn;
        private readonly Producer producer;
        private IndicateBattery indicateBattery;
        private IListener _udp;


        public DataController(BlockingCollection<DataContainerMeasureVals> dataQueueMeasure, BlockingCollection<DataContainerUdp> dataQueueLimit, BlockingCollection<DataContainerUdp> dataQueueCommands)
        {
            _udpSender = new UdpSender();
            _alarm = new FakeAlarm();
            _adc = new ReceiveAdc();
            indicateBattery = new IndicateBattery();
            producer = new Producer(dataQueueMeasure);
            _udp = new UdpListener(dataQueueLimit, dataQueueCommands);
        }

        public void StartUdpLimit()
        {
            _udp.ListenLimitValsPC();
        }
        public void ReceiveSystemOn(bool systemOn)
        {
            _systemOn = systemOn;
            _udp.ReceiveSystemOn(_systemOn);
        }
        public List<double> StartCal()
        {
            calDoubles = new List<double>(875);
            calDoubles = _adc.MeasureCalibration();
            return calDoubles;
        }
        public List<double> StartZeroAdjust()
        {
            calDoubles = new List<double>(875);
            calDoubles = _adc.MeasureZeroAdjust();
            return calDoubles;
        }

        public void StartMeasure()
        {
            producer.RunMeasure();
        }


        public void SendMeanCal(double meanVal)
        {
            _udpSender.SendDouble(meanVal);
        }


        public void SendZero(double zeroAdjustMean)
        {
            _udpSender.SendDouble(zeroAdjustMean);
        }


        public void SendRaw(List<DTO_Raw> raw)  
        {
            _udpSender.SendDTO_Raw(raw);
        }


        public void SendDTOCalcualted(DTO_Calculated dtoCalculated)
        {
            _udpSender.SendDTO_Calculated(dtoCalculated);
        }

        public void AlarmRequestStart(object alarmType)
        {
            string _alarmType = (string)alarmType;
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

        public void IndicateLowBattery()
        {
            indicateBattery.IndicateLowBattery();
        }

        public void TurnOffLed()
        {
            indicateBattery.TurnOff();
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


        public double GetBatterystatus()
        {
            return _adc.MeasureBattery();
        }

     
        public void UdpListenerListen()
        {
            _udp.ListenCommandsPC();
        }
    }
}
