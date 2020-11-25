﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO_s;
using DataAccessLogic;

namespace BusinessLogic
{
    
    public class Processing
    {
        /// <summary>
        /// den udregnede systoliske værdi, udregnet ved at tage max af 182 målepunkter svarende til tre målepunkter
        /// </summary>
        private int _calcualtedSys;
        /// <summary>
        /// den udregnede diastoliske værdi, udregnet ved at tage min af 182 målepunkter svarende til tre målepunkter
        /// </summary>
        private int _calculatedDia;
        /// <summary>
        /// den udregnede middel værdi af blodtrykket, udregnet ved at tage min af 182 målepunkter svarende til tre målepunkter
        /// </summary>
        private int _calculatedMean;
        /// <summary>
        /// den udregnede puls
        /// </summary>
        private int _calculatedPulse;
        /// <summary>
        /// består af et målepunkt og tiden dertil
        /// </summary>
        private DTO_Raw _raw;
        
       



        public DTO_Raw MakeDtoRaw(in double rawData, in double calibrationValue, in double zeroAdjustMean)
        {
            
            _raw = new DTO_Raw(ConvertBp(rawData,calibrationValue, zeroAdjustMean), DateTime.Now);
            return _raw;
        }


        /// <summary>
        /// omregner bp-værdien fra V til mmHg og tager højde for nulpunkjusteringen
        /// laver en liste til af 10(overvej om der skal flere målepunkter til når det er en rigtig måling) målepunkter
        /// Opretter DTO_calculated objektet med tilhørende parametre
        /// </summary>
        public double ConvertBp(double rawData, double calibrationval, double ZeroAdjustVal)
        {
            rawData = (rawData / 559 / 5 / 0.000005) * calibrationval - ZeroAdjustVal;
            return rawData;
        }

        /// <summary>
        /// Udregner den systoliske værdi for blodtrykket, ved at tage listen af ti(!!!!! kan ændres) målepunkter og finde max
        /// </summary>
        /// <returns>den udregnedende systoliske værdi</returns>

        public int CalculateSys(List<double> bpList)
        {
           _calcualtedSys=Convert.ToInt32(bpList.Max());
           return _calcualtedSys;
        }
        /// <summary>
        /// Udregner den diastoliske værdi for blodtrykket, ved at tage listen af ti(!!!!! kan ændres) målepunkter og finde min
        /// </summary>
        /// <returns>den udregnedende diastoliske værdi</returns>
        public int CalculateDia(List<double> bpList)
        {
            _calculatedDia = Convert.ToInt32(bpList.Min());
            return _calculatedDia;
        }
        public int CalculateMean(List<double> bpList)
        {
            _calculatedMean = Convert.ToInt32(bpList.Average());
            return _calculatedMean;
        }
        /// <summary>
        /// Udregner pulsen ved at tage listen på 3 sekunders samples og se hvor mange gange vi kommer forbi meanvalue, dividere med 2(for at tage højde for at den passere både op og ned), og gange med 20 så vi får en puls, som er beats pr minuts.
        /// Denne metode er en meget simpel udregning af pulsen, og det kunne have været udregnet på en mere præcis måde, men prioritereingen har valgt denne metode
        /// </summary>
        /// <returns>den udregnedende puls</returns>
        public int CalculatePulse(List<double> bpList, int mean)
        {
            var intList = bpList.Select(s => Convert.ToInt32(s)).ToList();

            int countOfMean= CountOccurenceOfValue(intList, mean);
            _calculatedPulse = (countOfMean / 2)* 20;
            
            return _calculatedPulse;
            
        }
        static int CountOccurenceOfValue(List<int> list, int valueToFind)
        {
            return ((from temp in list where temp.Equals(valueToFind) select temp).Count());
        }


        public DTO_BP CalculateData(List<double> bpList)
        {
            DTO_BP calculated = new DTO_BP(CalculateSys(bpList), CalculateDia(bpList), CalculateMean(bpList), CalculatePulse(bpList, _calculatedMean));
            return calculated;
        }
    }
}
