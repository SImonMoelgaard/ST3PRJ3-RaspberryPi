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
       
        private readonly UdpSender _udpSender= new UdpSender();
        private readonly Alarm _alarm= new Alarm();
        private readonly IBPData _adc= new ReceiveAdc();
        private List<double> calDoubles= new List<double>();
        private bool _systemOn = true;
        private readonly BlockingCollection<DataContainerMeasureVals> _dataQueue;

        public DataController(BlockingCollection<DataContainerMeasureVals> dataQueue)
        {
            _dataQueue = dataQueue;
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

        public void SendRaw(List<DTO_Raw> _rawList)
        {
            
            List<double> _bpList = new List<double>();

           _udpSender.SendDTO_Raw(_rawList);
           
           while (_systemOn)
           {
               DataContainerMeasureVals dataContainer= new DataContainerMeasureVals();
               foreach (var BP in _rawList)
               {
                   dataContainer.SetMeasureVal(BP.mmHg);
                  _dataQueue.Add(dataContainer);
                   Thread.Sleep(10); // Ved egentlig ikke om den skal "sleep" 
               }
           }
           _dataQueue.CompleteAdding();



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
    }
}
