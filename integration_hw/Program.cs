using RaspberryPiNetCore.ADC;
using RaspberryPiNetCore.LCD;
using RaspberryPiNetCore.TWIST;
using System;
using DataAccessLogic;


namespace Raspberry_Pi_Dot_Net_Core_Console_Application3
{
    class Program
    {
        private static ReceiveAdc _adc = new ReceiveAdc();
        private static IndicateBattery indicateBattery= new IndicateBattery();
        private static Alarm alarm=new Alarm();
        static void Main(string[] args)
        {
            Console.WriteLine("Test af Measure: differential");
            var measuredVal = _adc.Measure();
            Console.WriteLine("Den målte værdi er: " + measuredVal);
            Console.ReadLine();
            Console.WriteLine("Test af Battery: Single");
            var batteryVal = _adc.MeasureBattery();
            Console.WriteLine("Batteri i Volt er: " + batteryVal);
            Console.ReadLine();
            Console.WriteLine("Test af Calibration: Single");
            var calVal = _adc.MeasureCalibration();
            foreach (var VARIABLE in calVal)
            {
                Console.WriteLine("Kalibreringsværdi: "+ VARIABLE);
            }

            Console.ReadLine();
            Console.WriteLine("Nu burde LED'en lyse");
            indicateBattery.IndicateLowBattery();
            Console.WriteLine("Tryk på en tast for at slukke");
            Console.ReadLine();
            indicateBattery.TurnOff();

            Console.WriteLine("Test af højtaler");
            Console.ReadLine();
            alarm.StartHighAlarm();
            Console.WriteLine("Tryk for at slukke igen");
            Console.ReadLine();
            alarm.StopHighAlarm();

        }
    }
}
