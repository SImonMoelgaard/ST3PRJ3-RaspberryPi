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
        private List<double> _calDoubles;
        private bool _systemOn;
        private readonly Producer _producer;
        private readonly IndicateBattery _indicateBattery;
        private readonly IListener _udpListener;


        public DataController(BlockingCollection<DataContainerMeasureVals> dataQueueMeasure, BlockingCollection<DataContainerUdp> dataQueueLimit, BlockingCollection<DataContainerUdp> dataQueueCommands)
        {
            _udpSender = new UdpSender();
            _alarm = new FakeAlarm();
            _adc = new ReceiveAdc();
            _indicateBattery = new IndicateBattery();
            _producer = new Producer(dataQueueMeasure);
            _udpListener = new UdpListener(dataQueueLimit, dataQueueCommands);
        }

        public void StartUdpLimit()
        {
            _udpListener.ListenLimitValsPC();
        }

        public void ReceiveSystemOn(bool systemOn)
        {
            _systemOn = systemOn;
            _udpListener.ReceiveSystemOn(_systemOn);
        }

        public List<double> StartCal()
        {
            _calDoubles = new List<double>(875);
            _calDoubles = _adc.MeasureCalibration();
            return _calDoubles;
        }

        public List<double> StartZeroAdjust()
        {
            _calDoubles = new List<double>(875);
            _calDoubles = _adc.MeasureZeroAdjust();
            return _calDoubles;
        }

        public void StartMeasure()
        {
            _producer.RunMeasure();
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
            _indicateBattery.IndicateLowBattery();
        }

        public void TurnOffLed()
        {
            _indicateBattery.TurnOff();
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
            _udpListener.ListenCommandsPC();
        }
    }
}
