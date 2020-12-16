using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using DTO_s;

namespace DataAccessLogic
{
    public class FakeAdc : IBPData
    {
        /// <summary>
        /// er en tilfældig værdi i det range, vi ville få fra måleren
        /// </summary>
        private double _mmHgAsV;

        private int count;
        private const int fivesec = 175 * 5;
        private List<double> _zeroAdjustVals;
        private List<double> calibrationVals;
        private DTO_Raw raw;


        /// <summary>
        /// denne metode er til at teste værdierne i systemet og ligner virkeligheden hvorr vi kun får en blodtryksværdi af gangen
        /// </summary>
        /// <returns>en random short i det range, vi kan få fra HWen</returns>
        public DTO_Raw Measure()
        {
            Random random = new Random();

            _mmHgAsV = 250 * random.NextDouble();
            raw = new DTO_Raw(_mmHgAsV, DateTime.Now);

            Thread.Sleep(20); //Her skal der retts til så det passer til vores system
            //_mmHgAsV = 75;
            return raw;
            //_raw = new DTO_Raw(_mmHgAsV, DateTime.Now);
        }

        public double MeasureBattery()
        {
            Random random = new Random();
            return 1;
        }
        public double NewMeasureZeroAdjust()
        {

            Random random = new Random();
            return random.Next(4000);
        }


        public double NewMeasureCalibration()
        {

            Random random = new Random();
            return random.Next(4000);
        }

        /// <summary>
        /// Metode til kalibrering der laver 1 måling over x sekunder og returnerer en double-værdi 
        /// </summary>
        /// <returns></returns>
        public List<double> MeasureCalibration()
        {
            calibrationVals = new List<double>();
            count = 0;
            Random random = new Random();
            while (count != fivesec)
            {


                //foreach (var calVal in calibrationVals)
                //{
                calibrationVals.Add(Convert.ToInt16(random.Next(4)));
                count++;
                // }
            }

            return calibrationVals;
        }

        /// <summary>
        /// Modtager og returnerer 10 målinger til nulpunktsjustering
        /// </summary>
        /// <returns> liste med 910 målinger </returns>
        public List<double> MeasureZeroAdjust()
        {
            _zeroAdjustVals = new List<double>(fivesec);
            Console.WriteLine("RFF measure zero");
            count = 0;
            while (count != fivesec)
            {
                Random random = new Random();
                //foreach (var zeroAdjust in _zeroAdjustVals)
                //{
                _zeroAdjustVals.Add(Convert.ToInt16(random.Next(4)));
                count++;
                //  }
            }

            return _zeroAdjustVals;

        }


    }

}


