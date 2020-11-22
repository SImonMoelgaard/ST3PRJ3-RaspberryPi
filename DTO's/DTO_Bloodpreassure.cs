using System;
using System.Collections.Generic;
using System.Text;

namespace DTO_s
{
    public class DTO_Bloodpreassure
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
        public int CalculatedPulse
        {
            get;
            set;
        }

        public int CalculatedMean
        {
            get;
            set;
        }

        public DTO_Bloodpreassure(int sys, int dia, int mean, int pulse)
        {
            CalculatedSys = sys;
            CalculatedDia = dia;
            CalculatedPulse = pulse;
            CalculatedMean = mean;

        }
    }
}
