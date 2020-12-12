using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using RaspberryPiCore;
using DTO_s;

namespace DataAccessLogic
{
    public class DataController
    {
       
        private readonly ISender _udpSender= new FakeSender();
        private readonly IAlarm _alarm= new FakeAlarm();
        private readonly IBPData _adc= new FakeAdc();
        private List<double> meanDoubles= new List<double>();
        private bool _systemOn;
        private readonly Producer producer;
        private  IndicateBattery indicateBattery= new IndicateBattery();
       // private readonly BlockingCollection<DataContainerMeasureVals> _dataQueueVals;

        public DataController(BlockingCollection<DataContainerMeasureVals> dataQueueMeasure, BlockingCollection<DataContainerUdp> dataQueueLimit, BlockingCollection<DataContainerUdp> dataQueueCommands)
        {
           // _dataQueueVals = dataQueueMeasure;

            producer= new Producer(dataQueueLimit,dataQueueCommands,dataQueueMeasure);
        }

        public void ReceiveSystemOn(bool systemOn)
        {
            _systemOn = systemOn;
            producer.ReceiveSystemOn(_systemOn);
        }
        public List<double> StartCal()
        {
            meanDoubles =_adc.MeasureCalibration();
            return meanDoubles;

        }
        public List<double> StartZeroAdjust()
        {
            meanDoubles = _adc.MeasureZeroAdjust();
            return meanDoubles;

        }
        //public void NewStartZeroAdjust()
        //{
        //    producer.RunZero();
        //}

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
            while (_alarmType == "highSys")
            {
                _alarm.StartMediumAlarm();
            }

            while (_alarmType == "lowMean")
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
            producer.RunCommand();
        }
    }
}
