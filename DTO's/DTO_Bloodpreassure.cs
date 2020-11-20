using System;
using System.Collections.Generic;
using System.Text;

namespace DTO_s
{
    public class DTO_Bloodpreassure
    {
        private int CalculatedSys
        {
            get;
            set;
        }

        private int CalculatedDia
        {
            get;
            set;
        }
        private int CalculatedPulse
        {
            get;
            set;
        }

        private int CalculatedMean
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
