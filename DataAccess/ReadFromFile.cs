using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using DTO_s;

namespace DataAccessLogic
{
    public class ReadFromFile : IBPData
    {
        /// <summary>
        /// er en tilfældig værdi i det range, vi ville få fra måleren
        /// </summary>
        private double _mmHgAsV;

        private int count;

        private readonly List<double> _zeroAdjustVals = new List<double>(10);
        private readonly List<double> calibrationVals = new List<double>(910);


        /// <summary>
        /// denne metode er til at teste værdierne i systemet og ligner virkeligheden hvorr vi kun får en blodtryksværdi af gangen
        /// </summary>
        /// <returns>en random short i det range, vi kan få fra HWen</returns>
        public double Measure()
        {
            Random random = new Random();

            _mmHgAsV = 3.5 * random.NextDouble();
            Thread.Sleep(20); //Her skal der retts til så det passer til vores system
            return Convert.ToInt16(_mmHgAsV);
            //_raw = new DTO_Raw(_mmHgAsV, DateTime.Now);
        }

        public double MeasureBattery()
        {
            //Random random = new Random();
            return 1;
        }

        /// <summary>
        /// Metode til kalibrering der laver 1 måling over x sekunder og returnerer en double-værdi 
        /// </summary>
        /// <returns></returns>
        public List<double> MeasureCalibration()
        {
            count = 0;
            Random random = new Random();
            while (count != calibrationVals.Capacity)
            {


                foreach (var calVal in calibrationVals)
                {
                    calibrationVals.Add(3.5 * (random.NextDouble()));
                }
            }

            return calibrationVals;
        }

        /// <summary>
        /// Modtager og returnerer 10 målinger til nulpunktsjustering
        /// </summary>
        /// <returns> liste med 910 målinger </returns>
        public List<double> MeasureZeroAdjust()
        {
            //count = 0;
            //while (count != _zeroAdjustVals.Capacity/4)
            //{
            //    _zeroAdjustVals.Add(1);
            //    _zeroAdjustVals.Add(2);
            //    _zeroAdjustVals.Add(3);
            //    _zeroAdjustVals.Add(4);
            //    //Random random = new Random();
            //    //foreach (var zeroAdjust in _zeroAdjustVals)
            //    //{
            //    //    _zeroAdjustVals.Add(3.5*(random.NextDouble()));
            //    //}
            //}

            return _zeroAdjustVals;

        }

    
}

}


