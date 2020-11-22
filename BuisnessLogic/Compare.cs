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

        private int _highSys;
        private int _lowSys;
        private int _highDia;
        private int _lowDia;
        private int _highMean;
        private int _lowMean;
        private DTO_exceedVals exceedVals;
       


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

        }

        /// <summary>
        ///  Tjekker om grænseværdierne er overskredet og returnerer en alarmtype.
        /// </summary>
        /// <returns>alarmtype as an int</returns>


        public DTO_exceedVals LimitValExceeded(DTO_Bloodpreassure Bp)
        {
            exceedVals = new DTO_exceedVals(false, false,false,false,false, false);
            if (Bp.CalculatedSys >= _highSys)
            {
                exceedVals.HighSys = true;
            }
            if (Bp.CalculatedSys <= _lowSys)
            {
                exceedVals.LowSys = true;
            }
            if (Bp.CalculatedDia >= _highDia)
            {
                exceedVals.HighDia = true;
            }
            if (Bp.CalculatedDia <= _lowDia)
            {
                exceedVals.LowDia = true;
            }
            if (Bp.CalculatedMean >= _highMean)
            {
                exceedVals.HighMean = true;
            }
            if (Bp.CalculatedMean <= _lowMean)
            {
                exceedVals.LowMean = true;
            }
            
            return exceedVals; 

        }
    }
}
