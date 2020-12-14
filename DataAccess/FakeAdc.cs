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
        private ushort _mmHgAsV;

        private int count;

        private readonly List<short> _zeroAdjustVals = new List<short>(910);
        private readonly List<short> calibrationVals = new List<short>(910);


        /// <summary>
        /// denne metode er til at teste værdierne i systemet og ligner virkeligheden hvorr vi kun får en blodtryksværdi af gangen
        /// </summary>
        /// <returns>en random short i det range, vi kan få fra HWen</returns>
        public ushort Measure()
        {
            Random random = new Random();

            _mmHgAsV = 1;
            Thread.Sleep(20); //Her skal der retts til så det passer til vores system
            return _mmHgAsV;
            //_raw = new DTO_Raw(_mmHgAsV, DateTime.Now);
        }

        public ushort MeasureBattery()
        {
            Random random = new Random();
            return 1;
        }

        /// <summary>
        /// Metode til kalibrering der laver 1 måling over x sekunder og returnerer en double-værdi 
        /// </summary>
        /// <returns></returns>
        public List<short> MeasureCalibration()
        {
            count = 0;
            Random random = new Random();
            while (count != calibrationVals.Capacity)
            {


                foreach (var calVal in calibrationVals)
                {
                    calibrationVals.Add(Convert.ToInt16(random.Next(4)));
                }
            }

            return calibrationVals;
        }

        /// <summary>
        /// Modtager og returnerer 10 målinger til nulpunktsjustering
        /// </summary>
        /// <returns> liste med 910 målinger </returns>
        public List<short> MeasureZeroAdjust()
        {
            count = 0;
            while (count != _zeroAdjustVals.Capacity)
            {
                Random random = new Random();
                foreach (var zeroAdjust in _zeroAdjustVals)
                {
                    _zeroAdjustVals.Add(Convert.ToInt16(random.Next(4)));
                }
            }

            return _zeroAdjustVals;

        }


    }

}


