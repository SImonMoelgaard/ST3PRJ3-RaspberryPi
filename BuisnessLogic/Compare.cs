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
        //private int alarmType;
        
        public bool[] exceededVals = new bool[8];
        private int _highSys;
        private int _lowSys;
        private int _highDia;
        private int _lowDia;
        private int _highMean;
        private int _lowMean;
        private int _highPulse;
        private int _lowPulse;

        /// <summary>
        /// Sætter grænseværdierne til parametrerne 
        /// </summary>
        /// <param name="sys"> den systoliske (øvre) grænseværdi </param>
        /// <param name="meanBP"> grænseværdien (nedre) for middelblodtrykket </param>
        public void SetLimitVals(DTO_LimitVals limitVals)
        {
            _highSys =limitVals.HighSys;
            _lowSys = limitVals.LowSys;
            _highDia = limitVals.HighDia;
            _lowDia = limitVals.LowDia;
            _highMean = limitVals.HighMean;
            _lowMean = limitVals.LowMean;
            _highPulse = limitVals.HighPulse;
            _lowPulse = limitVals.LowPulse;
        }

        /// <summary>
        ///  Tjekker om grænseværdierne er overskredet og returnerer en alarmtype.
        /// </summary>
        /// <returns>alarmtype as an int</returns>

        public bool[] LimitValExceeded(DTO_Bloodpreassure Bp)
        {
            if (Bp.CalculatedSys >= _highSys)
            {
                exceededVals[1] = true;
            }
            if (Bp.CalculatedSys >= _lowSys)
            {
                exceededVals[2] = true;
            }
            if (Bp.CalculatedDia >= _highDia)
            {
                exceededVals[3] = true;
            }
            if (Bp.CalculatedDia >= _lowDia)
            {
                exceededVals[4] = true;
            }
            if (Bp.CalculatedMean >= _highMean)
            {
                exceededVals[5] = true;
            }
            if (Bp.CalculatedMean >= _lowMean)
            {
                exceededVals[6] = true;
            }
            if (Bp.CalculatedPulse >= _highPulse)
            {
                exceededVals[7] = true;
            }
            if (Bp.CalculatedPulse >= _lowPulse)
            {
                exceededVals[8] = true;
            }
            return exceededVals; 
        }
    }
}
