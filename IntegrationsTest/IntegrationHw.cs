using System;
using DataAccessLogic;


namespace IntegrationsTest
{
    public class IntegrationHw
    {

        private static ReceiveAdc _adc = new ReceiveAdc();
        private static IndicateBattery indicateBattery = new IndicateBattery();
        private static Alarm alarm = new Alarm();

        public void MeasureTest()
        {
            Console.WriteLine("Test af Measure: differential");
            var measuredVal = _adc.Measure();
            Console.WriteLine("Den målte værdi er: " + measuredVal);
            Console.ReadLine();
            Console.WriteLine("Test af Battery: Single");
            var batteryVal = _adc.MeasureBattery();
            Console.WriteLine("Batteri i Volt er: " + batteryVal);
            Console.ReadLine();
            Console.WriteLine("Test af DoCalibration: Single");
            var calVals = _adc.MeasureCalibration();
            foreach (var VARIABLE in calVals)
            {
                Console.WriteLine("Kalibreringsværdi: " + VARIABLE);
            }
        }

        public void LedTest()
        {
            Console.WriteLine("Nu burde LED'en lyse");
            indicateBattery.IndicateLowBattery();
            Console.WriteLine("Tryk på en tast for at slukke");
            Console.ReadLine();
            indicateBattery.TurnOff();
        }

        public void SpeakerTest()
        {
            Console.WriteLine("Test af højtaler");
            Console.ReadLine();
            alarm.StartHighAlarm();
            Console.WriteLine("Tryk for at slukke igen");
            Console.ReadLine();
            alarm.StopHighAlarm();
        }
       

    



    }
}
