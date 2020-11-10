using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO_s;
using DataAccessLogic;

namespace BuisnessLogic
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
        /// <summary>
        /// den udregnede puls, udregnet ved ????? TODO HOW DOD WE DO THIS??
        /// </summary>
        private int calculatedPulse;
        /// <summary>
        /// et objekt af interfacet IBPData, som enten læser fra en fil eller fra måleren
        /// </summary>
        private IBPData rADCObj = new ReceiveADC();
        /// <summary>
        /// består af et målepunkt og tiden dertil
        /// </summary>
        private DTO_Raw raw;
        /// <summary>
        /// Liste bestående af 10 målinger med tidspunkt
        /// </summary>
        private List<DTO_Raw> bpList=new List<DTO_Raw>(10);
        /// <summary>
        /// Liste bestående af 10 "rene" målinger
        /// </summary>
        private List<double> bpVals = new List<double>(10);
        /// <summary>
        /// zeroajustment objekt, for at få adgang til nulpunktsjusteringen
        /// </summary>
        private ZeroAdjustment zeroObj = new ZeroAdjustment();
        /// <summary>
        /// batterystatus objekt, for at få adgang til batteristatusen, som skal med i DTO_calculated
        /// </summary>
        private BatteryStatus batObj =new BatteryStatus();
        /// <summary>
        /// Compare objekt, for at få adgang til Alarmtypen, som skal med i DTO_calculated
        /// </summary>
        private Compare comObj = new Compare();
        /// <summary>
        /// receiveUI objekt, for at få adgang til Calibreringsjusteringen, som så skal ganges på blodtrykket
        /// </summary>
        private ReceiveUI RUIObj = new ReceiveUI();
        /// <summary>
        /// DTO_calculated objekt, som senere får alle informationerne, der skal sendes videre til UI
        /// </summary>
        private DTO_Calculated CalculatedObj;
        





        /// <summary>
        /// omregner bp-værdien fra V til mmHg og tager højde for nulpunkjusteringen
        /// laver en liste til af 10(overvej om der skal flere målepunkter til når det er en rigtig måling) målepunkter
        /// Opretter DTO_calculated objektet med tilhørende parametre
        /// </summary>
        public void ConvertVtoBP() //Tænk over bedre navn :D - Hvad siger du til det her Ans?? <3333 SHIT ET GODT NAVN NU :D
        {
           raw = rADCObj.MeassureSignal();
            raw.mmHg = (raw.mmHg / 559 / 5 / 0.000005)* RUIObj.ReceiveCalibrationVal() - zeroObj.CalculateZeroVal();
            bpList.Add(raw);
            bpVals.Add(bpList[0].mmHg);//får vi et problem her?? sætter denne metode ikke altid værdien på index 0?
           
        }

        public DTO_Calculated MakeDTOCalculated()
        {
           return CalculatedObj = new DTO_Calculated(CalculateSys(), CalculateDia(), CalculatePulse(), batObj.CalculateBatteryStatus(), comObj.LimitValExceeded());
        }

        /// <summary>
        /// Udregner den systoliske værdi for blodtrykket, ved at tage listen af ti(!!!!! kan ændres) målepunkter og finde max
        /// </summary>
        /// <returns>den udregnedende systoliske værdi</returns>

        public int CalculateSys()
        {
           calcualtedSys=Convert.ToInt32(bpVals.Max());
           return calcualtedSys;
        }
        /// <summary>
        /// Udregner den diastoliske værdi for blodtrykket, ved at tage listen af ti(!!!!! kan ændres) målepunkter og finde min
        /// </summary>
        /// <returns>den udregnedende diastoliske værdi</returns>
        public int CalculateDia()
        {
            calculatedDia = Convert.ToInt32(bpVals.Min());
            return calculatedDia;
        }
        /// <summary>
        /// Udregner pulsen TODO HOW????
        /// </summary>
        /// <returns>den udregnedende puls</returns>
        public int CalculatePulse()
        {

            return calculatedPulse;
        }

    }
}
