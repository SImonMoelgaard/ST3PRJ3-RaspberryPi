using System;
using System.Collections.Generic;
using System.Text;

namespace DTO_s
{
    public class DTO_Calculated
    {
        public int CalculatedSys
        {
            get;
            set;
        }

        public int CalculatedDia
        {
            get;
            set;
        }
        public int CalculatedMean
        {
            get;
            set;
        }
        public int CalculatedPulse
        {
            get;
            set;
        }
        public bool HighSys
        {
            get;
            set;
        }

        public bool LowSys
        {
            get;
            set;
        }

        public bool HighDia
        {
            get;
            set;
        }

        public bool LowDia
        {
            get;
            set;
        }
        public bool HighMean
        {
            get;
            set;
        }

        public bool LowMean
        {
            get;
            set;
        }
        public int Batterystatus
        {
            get;
            set;
        }



        public DTO_Calculated(bool highSys, bool lowSys, bool highDia, bool lowDia, bool highMean, bool lowMean, int sys, int dia, int mean, int pulse, int batterystatus)
        {
            HighSys = highSys;
            LowSys = lowSys;
            HighDia = highDia;
            LowDia = lowDia;
            HighMean = highMean;
            LowMean = lowMean;
            CalculatedSys = sys;
            CalculatedDia = dia;
            CalculatedMean = mean;
            CalculatedPulse = CalculatedPulse;
            Batterystatus = batterystatus;

        }
    }
}
