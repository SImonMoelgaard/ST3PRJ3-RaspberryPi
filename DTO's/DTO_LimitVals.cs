using System;
using System.Collections.Generic;
using System.Text;

namespace DTO_s
{
    public class DTO_LimitVals
    {
        public int HighSys
        {
            get;
            set;
        }

        public int LowSys
        {
            get;
            set;
        }

        public int HighDia
        {
            get;
            set;
        }

        public int LowDia
        {
            get;
            set;
        }
        public int HighMean
        {
            get;
            set;
        }

        public int LowMean
        {
            get;
            set;
        }
        public int HighPulse
        {
            get;
            set;
        }

        public int LowPulse
        {
            get;
            set;
        }


        public DTO_LimitVals(int highSys, int lowSys, int highDia, int lowDia, int highMean, int lowMean, int highPulse, int lowPulse)
        {
            HighSys = highSys;
            LowSys = lowSys;
            HighDia = highDia;
            LowDia = lowDia;
            HighMean = highMean;
            LowMean = lowMean;
            HighPulse = highPulse;
            LowPulse = lowPulse;
        }
    }
}
