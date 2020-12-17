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
        
        private double _mmHgAsV;
        private int count;
        private const int fivesec = 175 * 5;
        private List<double> _zeroAdjustVals;
        private List<double> _calibrationVals;
        private DTO_Raw raw;
        string[] inputArray;

        /// <summary>
        /// denne metode er til at teste værdierne i systemet og ligner virkeligheden hvorr vi kun får en blodtryksværdi af gangen
        /// </summary>
        /// <returns>en random double i det range, vi kan få fra HWen</returns>
        public DTO_Raw Measure()
        {
            //Random random = new Random();

            //_mmHgAsV = 250 * random.NextDouble();
            //raw = new DTO_Raw(_mmHgAsV, DateTime.Now);

            //Thread.Sleep(20); 
            //return raw;

            int count = 0;
            FileStream input = new FileStream(@"" + "Sample.txt", FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(input);

            
                string inputRecord = reader.ReadLine();
                inputArray = inputRecord.Split(',');
                _mmHgAsV=(Convert.ToDouble(inputArray[1]) / 1000);
                raw= new DTO_Raw(_mmHgAsV,DateTime.Now);
                count++;
                input.Close();
                return raw;
        }

        public double MeasureBattery()
        {
            return 1;
        }

        /// <summary>
        /// Metode til kalibrering der laver målinger over 5 sekunder og returnerer en liste med doubles
        /// </summary>
        /// <returns></returns>
        public List<double> MeasureCalibration()
        {
            _calibrationVals = new List<double>();
            count = 0;
            Random random = new Random();
            while (count != fivesec)
            {
                _calibrationVals.Add(Convert.ToInt16(random.Next(4)));
                count++;
            }

            return _calibrationVals;
        }

        /// <summary>
        /// Modtager og returnerer 5 sekunders værd af målinger til nulpunktsjustering
        /// </summary>
        /// <returns> liste med 5 sekunders værd af målinger </returns>
        public List<double> MeasureZeroAdjust()
        {
            _zeroAdjustVals = new List<double>(fivesec);
            Random random = new Random();
            count = 0;
            while (count != fivesec)
            {
                _zeroAdjustVals.Add(Convert.ToInt16(random.Next(4)));
                count++;
            }

            return _zeroAdjustVals;

        }


    }

}


