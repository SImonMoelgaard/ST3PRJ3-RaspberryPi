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
        /// Liste bestående af 10 målinger med tidspunkt
        /// </summary>
        private List<double> bpList=new List<double>(10);



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
        /// Udregner pulsen TODO HOW????
        /// </summary>
        /// <returns>den udregnedende puls</returns>
        public int CalculatePulse()
        {

            return calculatedPulse;
        }


        public DTO_Calculated CalculateData(DTO_Raw rawData)
        {
            DTO_Calculated calculated = new DTO_Calculated(CalculateSys(), CalculateDia(), CalculateMean(), CalculateMean() );
            return calculated;
        }
    }
}
