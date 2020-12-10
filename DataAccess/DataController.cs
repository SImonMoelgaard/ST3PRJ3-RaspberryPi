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
       
        private readonly ISender _udpSender= new FakeSender();
        private readonly Alarm _alarm= new Alarm();
        private readonly IBPData _adc= new ReadFromFile();
        private List<double> calDoubles= new List<double>();
        private bool _systemOn;
        private readonly Producer producer;
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
           _adc.MeasureCalibration();
            return calDoubles;

        } 
        public List<double> StartZeroAdjust()
        {
            _adc.MeasureZeroAdjust();
            return calDoubles;

        }

        public double StartMeasure()
        {
            
            return _adc.Measure();
            
        }

        
        public void SendMeanCal(double meanVal)
        {
            _udpSender.SendDouble(meanVal);
        }

       
        public void SendZero(double zeroAdjustMean)
        {
            _udpSender.SendDouble(zeroAdjustMean);
        }


        public void SendRaw(List<DTO_Raw> _rawList) //Tråd her 
        {
            _udpSender.SendDTO_Raw(_rawList);
            producer.AddToQueue(_rawList);
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
