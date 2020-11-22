using System;
using System.Collections.Generic;
using System.Text;

namespace DTO_s
{
    public class DTO_exceedVals
    {
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



        public DTO_exceedVals(bool highSys, bool lowSys, bool highDia, bool lowDia, bool highMean, bool lowMean)
        {
            HighSys = highSys;
            LowSys = lowSys;
            HighDia = highDia;
            LowDia = lowDia;
            HighMean = highMean;
            LowMean = lowMean;
        }
    }
}
