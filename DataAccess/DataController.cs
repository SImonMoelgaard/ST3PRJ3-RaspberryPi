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

        private readonly ISender _udpSender = new UdpSender();
        private readonly IAlarm _alarm = new FakeAlarm();
        private readonly IBPData _adc = new ReceiveAdc();
        private List<double> calDoubles;
        private bool _systemOn;
        private readonly Producer producer;
        private IndicateBattery indicateBattery = new IndicateBattery();
        private IListener _udp;
        // private readonly BlockingCollection<DataContainerMeasureVals> _dataQueueVals;

        public DataController(BlockingCollection<DataContainerMeasureVals> dataQueueMeasure, BlockingCollection<DataContainerUdp> dataQueueLimit, BlockingCollection<DataContainerUdp> dataQueueCommands)
        {
            // _dataQueueVals = dataQueueMeasure;

            producer = new Producer(dataQueueLimit, dataQueueCommands, dataQueueMeasure);
            _udp = new UdpListener(dataQueueCommands);
        }

        public void startUDPUp()
        {
            _udp.ListenCommandsPC();
        }

        public void ReceiveSystemOn(bool systemOn)
        {
            _systemOn = systemOn;
            producer.ReceiveSystemOn(_systemOn);
        }
        public List<double> StartCal()
        {
            calDoubles = new List<double>(875);
            calDoubles = _adc.MeasureCalibration();
            return calDoubles;

        }
        public List<double> StartZeroAdjust()
        {
            Console.WriteLine("DC Startzwero");
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
            //_udpSender.SendCalDouble(meanVal);
            _udpSender.SendDouble(meanVal);
        }


        public void SendZero(double zeroAdjustMean)
        {
            Console.WriteLine("DC send zero" + zeroAdjustMean);
            _udpSender.SendDouble(zeroAdjustMean);
        }


        public void SendRaw(List<DTO_Raw> raw) //Tråd her 
        {
            _udpSender.SendDTO_Raw(raw);
            //producer.RunMeasure(raw);
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

        public void ProducerLimitRun()
        {
            producer.RunLimit(); //exeption her
        }

        public void ProducerCommandsRun()
        {
            //producer.RunCommand();
            //producer.newRunCommand();
            Console.WriteLine("DC producercomandsrun");
        }

        public void UdpListenerListen()
        {
            _udp.ListenCommandsPC();
        }
    }
}
