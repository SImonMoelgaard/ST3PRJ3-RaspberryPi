using System;
using System.Collections.Generic;
using System.Text;
using DTO_s;
using DataAccessLogic;

namespace BusinessLogic
{
    public class Compare
    {
        /// <summary>
        /// indikere hvilken type alarm der bliver udløst TODO tilføj hvornår de forskellige alarmtyper bliver udløst
        /// </summary>
        private int alarmType;

        private int highSys;
        private int lowSys;
        private int highDia;
        private int lowDia;
        private int highMean;
        private int lowMean;
        private int highPulse;
        private int lowPulse;

        /// <summary>
        /// Sætter grænseværdierne til parametrerne 
        /// </summary>
        /// <param name="sys"> den systoliske (øvre) grænseværdi </param>
        /// <param name="meanBP"> grænseværdien (nedre) for middelblodtrykket </param>
        public void SetLimitVals(DTO_LimitVals limitVals)
        {
            highSys =limitVals.HighSys;
            lowSys = limitVals.LowSys;
            highDia = limitVals.HighDia;
            lowDia = limitVals.LowDia;
            highMean = limitVals.HighMean;
            lowMean = limitVals.LowMean;
            highPulse = limitVals.HighPulse;
            lowPulse = limitVals.LowPulse;
        }

        /// <summary>
        ///  Tjekker om grænseværdierne er overskredet og returnerer en alarmtype.
        /// </summary>
        /// <returns>alarmtype as an int</returns>

        public int LimitValExceeded()
        {
            return alarmType;
        }
    }
}
