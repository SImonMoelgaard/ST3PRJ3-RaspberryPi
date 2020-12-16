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
    /// <summary>
    /// Denne klasse er controlleren på data acces logic. Herfra bliver alle de andre klasser på data acceslogic kaldt fra.
    /// </summary>
    public class DataController
    {

        private readonly ISender _udpSender;
        private readonly IAlarm _alarm;
        private readonly IBPData _adc;
        private List<double> _doubles;
        private bool _systemOn;
        private readonly Producer _producer;
        private readonly IndicateBattery _indicateBattery;
        private readonly IListener _udpListener;

        /// <summary>
        /// contructor for datacontrolleren. herigennem bliver de forskellige atributter og objekter oprettet
        /// </summary>
        /// <param name="dataQueueMeasure">datakø til blodtryksmålinger</param>
        /// <param name="dataQueueLimit"> datakø til grænseværdis DTO'er fra UI</param>
        /// <param name="dataQueueCommands"> datakø til comandoer fra UI</param>
        public DataController(BlockingCollection<DataContainerMeasureVals> dataQueueMeasure, BlockingCollection<DataContainerUdp> dataQueueLimit, BlockingCollection<DataContainerUdp> dataQueueCommands)
        {
            _udpSender = new UdpSender();
            _alarm = new FakeAlarm();
            _adc = new ReceiveAdc();
            _indicateBattery = new IndicateBattery();
            _producer = new Producer(dataQueueMeasure);
            _udpListener = new UdpListener(dataQueueLimit, dataQueueCommands);
        }
        /// <summary>
        /// Denne metode kalder Listenerens lytning på limitvals
        /// </summary>
        public void StartUdpLimit()
        {
            _udpListener.ListenLimitValsPC();
        }
        /// <summary>
        /// denne metode sætter en bool til at være true. når systemet er on
        /// </summary>
        /// <param name="systemOn">bool til indikation på om systemet er tændt</param>
        public void ReceiveSystemOn(bool systemOn)
        {
            _systemOn = systemOn;
            _udpListener.ReceiveSystemOn(_systemOn);
        }
        /// <summary>
        /// denne metode starter målingerne til senere udregning af calibrations værdi
        /// </summary>
        /// <returns>_doubles, som er en liste af målinger over 5 sekunder</returns>
        public List<double> StartCal()
        {
            _doubles = new List<double>(875);
            _doubles = _adc.MeasureCalibration();
            return _doubles;
        }
        /// <summary>
        /// denne metode starter målingerne til senere udregning af nulpunktjusterings værdi
        /// </summary>
        /// <returns>_doubles, som er en liste af målinger over 5 sekunder</returns>
        public List<double> StartZeroAdjust()
        {
            _doubles = new List<double>(875);
            _doubles = _adc.MeasureZeroAdjust();
            return _doubles;
        }
        /// <summary>
        /// denne metode påbegynder lytning på målinger fra adcen
        /// </summary>
        public void StartMeasure()
        {
            _producer.RunMeasure();
        }
        /// <summary>
        /// sender gennemsnittet af kalibreringsværdierne til senderen 
        /// </summary>
        /// <param name="meanVal">middelværdi af calibreringsværdierne</param>
        public void SendMeanCal(double meanVal)
        {
            _udpSender.SendDouble(meanVal);
        }
        /// <summary>
        /// sender gennemsnittet af nulpunktværdierne til senderen 
        /// </summary>
        /// <param name="zeroAdjustMean">middelværdi af nulpunktsværdierne</param>
        public void SendZero(double zeroAdjustMean)
        {
            _udpSender.SendDouble(zeroAdjustMean);
        }
        /// <summary>
        /// sender en liste af dto raw videre til senderen
        /// </summary>
        /// <param name="raw">liste bestående af blodtryksmålinger over et halvt sekund</param>
        public void SendRaw(List<DTO_Raw> raw)  
        {
            _udpSender.SendDTO_Raw(raw);
        }
        /// <summary>
        /// sender en DTO calculated, bestående af sys, dia, middel, puls, batteristatus, og overskridelse af grænseværdier videre
        /// </summary>
        /// <param name="dtoCalculated">bestående af sys, dia, middel, puls, batteristatus, og overskridelse af grænseværdier</param>
        public void SendDTOCalcualted(DTO_Calculated dtoCalculated)
        {
            _udpSender.SendDTO_Calculated(dtoCalculated);
        }
        /// <summary>
        /// beder alarmen om at starte
        /// </summary>
        /// <param name="alarmType">hvilken type alarm, der skal starte</param>
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
        /// <summary>
        /// muter alarmen i 5 minutter
        /// </summary>
        public void MuteAlarm()
        {
            _alarm.Mute();
        }
        /// <summary>
        /// tænder LED'en når batteristatus beder om det
        /// </summary>
        public void IndicateLowBattery()
        {
            _indicateBattery.IndicateLowBattery();
        }
        /// <summary>
        /// slukker LED igen
        /// </summary>
        public void TurnOffLed()
        {
            _indicateBattery.TurnOff();
        }
        /// <summary>
        /// stopper alarmen når grænseværdierne bliver overholdt
        /// </summary>
        /// <param name="alarmType">hvilken type alarm der skal slukkes for</param>
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
        /// <summary>
        /// får batteristatus i bits fra adcen
        /// </summary>
        /// <returns>batteristatus i bits</returns>
        public double GetBatterystatus()
        {
            return _adc.MeasureBattery();
        }
     /// <summary>
     /// starter lytningen efter commandoer i udplisteneren
     /// </summary>
        public void UdpListenerListen()
        {
            _udpListener.ListenCommandsPC();
        }
    }
}
