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
        private int calcualtedSys;
        private int calculatedDia;
        private int calculatedPulse;
        private ReceiveADC rADCObj = new ReceiveADC();
        private DTO_Raw raw;
        private List<DTO_Raw> bpList=new List<DTO_Raw>(10);
        private List<double> bpVals= new List<double>(10);


        //omregner bp-værdien fra mV til mmHg 
        public void BpAsmmHg() //Tænk over bedre navn :D
        {
            raw = rADCObj.MeassureSignal();
            //raw.mmHg = raw.mmHg * mvtommhg - nulpunktsjustering;
            bpList.Add(raw);
            bpVals.Add(bpList[0].mmHg);
        }



        //Udregner den systoliske værdi for blodtrykket
        public int CalculatedSys()
        {
           calcualtedSys=Convert.ToInt32(bpVals.Max());
           return calcualtedSys;
        }

        //udregner den diastoliske værdi for blodtrykket 
        public int CalculatedDia()
        {
            calculatedDia = Convert.ToInt32(bpVals.Min());
            return calculatedDia;
        }

        //udregner pulsen 
        public int CalculatedPulse()
        {

            return calculatedPulse;
        }

        
    }
}
