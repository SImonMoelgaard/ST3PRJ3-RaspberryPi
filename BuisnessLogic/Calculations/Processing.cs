using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO_s;
using DataAccessLogic;

namespace BusinessLogic
{

    public class Processing
    {
        
        private int _calcualtedSys;
        private int _calculatedDia;
        private int _calculatedMean;
        private int _calculatedPulse;
        
        /// <summary>
        /// modtager en liste med blodtryksmålinger i bits, med dertilhørende tidsstempel. hver blodtryksmåling, skal så omregnes til mmHg ved hjælp af nulpunkjusteringen og calibrationsværdien
        /// </summary>
        /// <param name="measureVals">liste med DTO_raws over et halvt sekund. blodtryk i bits</param>
        /// <param name="calibrationVal">kalibrationsværdi, der skal ganges med for at få blodtrykket i mmHg i stedet for bits</param>
        /// <param name="zeroAdjustVal">trækkes fra for at tage højde for det atmosfæriske tryk</param>
        /// <returns></returns>
        public List<DTO_Raw> MakeDtoRaw(List<DTO_Raw> measureVals, double calibrationVal, double zeroAdjustVal)
        {
            List<DTO_Raw> dtoRawList = new List<DTO_Raw>();

            foreach (var measure in measureVals)
            {
                measure.mmHg = measure.mmHg * calibrationVal - zeroAdjustVal;
            }
            return measureVals;
        }

        /// <summary>
        /// Udregner den systoliske værdi for blodtrykket, ved at tage listen af målinger over 3 sekunder og finde max heraf
        /// </summary>
        /// <returns>den udregnedende systoliske værdi</returns>
        public int CalculateSys(List<double> bpList)
        {
            _calcualtedSys = Convert.ToInt32(bpList.Max());
            Console.WriteLine("processing sys" + _calcualtedSys);
            return _calcualtedSys;
        }

        /// <summary>
        /// Udregner den diastoliske værdi for blodtrykket, ved at tage listen af målinger over 3 sekunder og finde minimum her af
        /// </summary>
        /// <returns>den udregnedende diastoliske værdi</returns>
        public int CalculateDia(List<double> bpList)
        {
            _calculatedDia = Convert.ToInt32(bpList.Min());
            Console.WriteLine("Processing" + _calculatedDia);
            return _calculatedDia;
        }
        /// <summary>
        /// Udregner den middelblodtrykket, ved at tage listen af målinger over 3 sekunder og finde gennemsnittet heraf
        /// </summary>
        /// <returns>den udregnedende diastoliske værdi</returns>
        public int CalculateMean(List<double> bpList)
        {
            _calculatedMean = Convert.ToInt32(bpList.Average());
            return _calculatedMean;
        }

        /// <summary>
        /// Udregner pulsen ved at tage listen på 3 sekunders samples og se hvor mange gange vi kommer forbi meanvalue, dividere med 2(for at tage højde for at den passeres både op og ned), og gange med 20 så vi får en puls, som er beats pr minut
        /// </summary>
        /// <returns>den udregnedende puls</returns>
        public int CalculatePulse(List<double> bpList, int mean)
        {
            var intList = bpList.Select(s => Convert.ToInt32(s)).ToList();

            int countOfMean = CountOccurenceOfValue(intList, mean);
            _calculatedPulse = (countOfMean / 2) * 60;

            return _calculatedPulse;

        }
        /// <summary>
        /// denne metode bruges til udregning af puls, til at se hvormange gange en bestemt værdi(mean) optræder i en liste. fået inspiration fra https://www.codeproject.com/Tips/69400/Count-number-of-occurences-of-a-value-in-a-List-us
        /// </summary>
        /// <param name="bpList">listen af blodtryk, der skal gåes igennem</param>
        /// <param name="mean">middelværdien, som vi gerne vil finde antallet af</param>
        /// <returns>antallet af gange, meanvalue bliver passeret</returns>
        static int CountOccurenceOfValue(List<int> bpList, int mean)
        {
            return ((from temp in bpList where temp.Equals(mean) select temp).Count());
        }
        /// <summary>
        /// kalder alle metoder til udregning af sys, dia, middel og puls
        /// </summary>
        /// <param name="bpList"></param>
        /// <returns></returns>
        public DTO_BP CalculateData(List<double> bpList)
        {
            DTO_BP calculated = new DTO_BP(CalculateSys(bpList), CalculateDia(bpList), CalculateMean(bpList), CalculatePulse(bpList, _calculatedMean));
            return calculated;
        }
    }
}
