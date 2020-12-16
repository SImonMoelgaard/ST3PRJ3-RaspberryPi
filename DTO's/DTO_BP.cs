using System;

namespace DTO_s
{ /// <summary>
/// Denne Klasse opretter et DTO objekt med Sys, Dia, Puls og middelblodtryk, så værdierne let kan flyttes rundt i det interne program
/// </summary>
    public class DTO_BP
    {
        public int CalculatedSys
        {
            get;
            set;
        }

        public int CalculatedDia {
            get;
            set;
        }
        public int CalculatedMean
        {
            get;
            set;
        }
        public int CalculatedPulse {
            get;
            set;
        }

        public DTO_BP(int sys, int dia,int mean, int pulse)
        {
            CalculatedSys = sys;
            CalculatedDia =dia;
            CalculatedMean = mean;
            CalculatedPulse=pulse;
        }
    }
}
