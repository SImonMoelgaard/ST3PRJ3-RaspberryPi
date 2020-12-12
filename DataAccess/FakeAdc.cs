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

        private readonly List<double> _zeroAdjustVals = new List<double>(910);
        private readonly List<double> calibrationVals = new List<double>(910);


        /// <summary>
        /// denne metode er til at teste værdierne i systemet og ligner virkeligheden hvorr vi kun får en blodtryksværdi af gangen
        /// </summary>
        /// <returns>en random short i det range, vi kan få fra HWen</returns>
        public double Measure()
        {
            if (count == 6)
            {
                _mmHgAsV = 7 * 0.5;
                count = 0;
            }
            else if (count == 5)
            {
                _mmHgAsV = 6*0.5;
                count = 6;
            }
            else if (count == 4)
            {
                _mmHgAsV = 5*0.5;
                count = 5;
            }
            else if (count == 3)
            {
                _mmHgAsV = 4*0.5;
                count = 4;
            }
            else if (count == 2)
            {
                _mmHgAsV = 1.5;
                count = 3;
            }
            else if (count == 1)
            {
                _mmHgAsV = 1.0;
                count = 2;
            }
            else if (count == 0)
            {
                _mmHgAsV = 0.5;
                count = 1;
            }
          
            //Random random = new Random();
            //_mmHgAsV = 3.5 * random.NextDouble();
            //Thread.Sleep(20); //Her skal der retts til så det passer til vores system
            //return Convert.ToInt16(_mmHgAsV);
            //_raw = new DTO_Raw(_mmHgAsV, DateTime.Now);
            return _mmHgAsV;
        }

        public double MeasureBattery()
        {
            //Random random = new Random();
            return 3;
        }

        /// <summary>
        /// Metode til kalibrering der laver 1 måling over x sekunder og returnerer en double-værdi 
        /// </summary>
        /// <returns></returns>
        public List<double> MeasureCalibration()
        {
            count = 0;
           // Random random = new Random();
            while (count != calibrationVals.Capacity/4)
            {
                calibrationVals.Add(1);
                calibrationVals.Add(3);
                calibrationVals.Add(2);
                calibrationVals.Add(4);

                //while (count !=cali)
                // {
                //calibrationVals.Add(3.5 * (random.NextDouble()));
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
            count = 0;
            while (count != _zeroAdjustVals.Capacity / 4)
            {
                _zeroAdjustVals.Add(1);
                _zeroAdjustVals.Add(2);
                _zeroAdjustVals.Add(3);
                _zeroAdjustVals.Add(4);
                //Random random = new Random();
                //foreach (var zeroAdjust in _zeroAdjustVals)
                //{
                //    _zeroAdjustVals.Add(3.5*(random.NextDouble()));
                count++;
                //}
            }

            return _zeroAdjustVals;

        }

    
}

}


