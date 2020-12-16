using System;
using System.Collections.Generic;
using System.Text;

namespace DTO_s
{
    /// <summary>
    /// Denne klasse definere hvad et DTO_ExceededVals er. exceededVals er til internt brug i RPi systemet. den indikerer om grænseværdierne er blevet overskredet. Denne vil senere blive slået sammen med DTO_BP i en DTO_Calculated for kun at sende en DTO til UI
    /// </summary>
    public class DTO_ExceededVals
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
        public DTO_ExceededVals(bool highSys, bool lowSys, bool highDia, bool lowDia, bool highMean, bool lowMean)
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
