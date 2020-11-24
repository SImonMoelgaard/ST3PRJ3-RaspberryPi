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
        
        private int _highSys;
        private int _lowSys;
        private int _highDia;
        private int _lowDia;
        private int _highMean;
        private int _lowMean;
        private DTO_ExceededVals _exceedVals;


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

        public DTO_ExceededVals LimitValExceeded(DTO_BP calculated)
        {
            _exceedVals = new DTO_ExceededVals(false, false,false,false,false, false);
            if (calculated.CalculatedSys >= _highSys)
            {
                _exceedVals.HighSys = true;
            }
            if (calculated.CalculatedSys <= _lowSys)
            {
                _exceedVals.LowSys = true;
            }
            if (calculated.CalculatedDia >= _highDia)
            {
                _exceedVals.HighDia = true;
            }
            if (calculated.CalculatedDia <= _lowDia)
            {
                _exceedVals.LowDia = true;
            }
            if (calculated.CalculatedMean >= _highMean)
            {
                _exceedVals.HighMean = true;
            }
            if (calculated.CalculatedMean <= _lowMean)
            {
                _exceedVals.LowMean = true;
            }
            
            return _exceedVals; 
        }
        
    }
}
