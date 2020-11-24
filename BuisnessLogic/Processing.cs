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
        /// <summary>
        /// den udregnede systoliske værdi, udregnet ved at tage max af ti målepunkter
        /// </summary>
        private int calcualtedSys;
        /// <summary>
        /// den udregnede diastoliske værdi, udregnet ved at tage min af ti målepunkter
        /// </summary>
        private int calculatedDia;

        private int calculatedMean;
        /// <summary>
        /// den udregnede puls, udregnet ved ????? TODO HOW DOD WE DO THIS??
        /// </summary>
        private int calculatedPulse;
        /// <summary>
        /// består af et målepunkt og tiden dertil
        /// </summary>
        private DTO_Raw raw;
        /// <summary>
        /// Liste bestående af 182 målinger
        /// </summary>
        private List<double> bpList=new List<double>(182);
       



        public DTO_Raw MakeDTORaw(in double rawData, in double calibrationValue, in double zeroAdjustMean)
        {
            
            raw = new DTO_Raw(ConvertBp(rawData,calibrationValue, zeroAdjustMean), DateTime.Now);
            bpList.Add(rawData);
            return raw;
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

        public int CalculateSys()
        {
           calcualtedSys=Convert.ToInt32(bpList.Max());
           return calcualtedSys;
        }
        /// <summary>
        /// Udregner den diastoliske værdi for blodtrykket, ved at tage listen af ti(!!!!! kan ændres) målepunkter og finde min
        /// </summary>
        /// <returns>den udregnedende diastoliske værdi</returns>
        public int CalculateDia()
        {
            calculatedDia = Convert.ToInt32(bpList.Min());
            return calculatedDia;
        }
        public int CalculateMean()
        {
            calculatedMean = Convert.ToInt32(bpList.Average());
            return calculatedMean;
        }
        /// <summary>
        /// Udregner pulsen ved at tage listen på 3 sekunders samples og se hvor mange gange vi kommer forbi meanvalue, dividere med 2(for at tage højde for at den passere både op og ned), og gange med 20 så vi får en puls, som er beats pr minuts.
        /// Denne metode er en meget simpel udregning af pulsen, og det kunne have været udregnet på en mere præcis måde, men prioritereingen har valgt denne metode
        /// </summary>
        /// <returns>den udregnedende puls</returns>
        public int CalculatePulse()
        {
            var intList = bpList.Select(s => Convert.ToInt32(s)).ToList();

            int countOfMean= CountOccurenceOfValue(intList, calculatedMean);
            calculatedPulse = (countOfMean / 2)* 20;
            
            return calculatedPulse;
            
        }
        static int CountOccurenceOfValue(List<int> list, int valueToFind)
        {
            return ((from temp in list where temp.Equals(valueToFind) select temp).Count());
        }


        public DTO_BP CalculateData(DTO_Raw rawData)
        {
            DTO_BP calculated = new DTO_BP(CalculateSys(), CalculateDia(), CalculateMean(), CalculateMean() );
            return calculated;
        }
    }
}
