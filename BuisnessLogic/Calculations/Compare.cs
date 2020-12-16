using System;
using System.Collections.Generic;
using System.Text;
using DTO_s;
using DataAccessLogic;

namespace BusinessLogic
{
    /// <summary>
    /// klasse til at sammenligne de udregnede blodtryksværdier med grænseværdierne
    /// </summary>
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
        private DTO_ExceededVals _exceededVals;



        /// <summary>
        /// Sætter grænseværdierne til parametrerne
        /// <summary>
        /// <param name="limitVals">de grænseværdier, der ikke må overskrides</param>
        public void SetLimitVals(DTO_LimitVals limitVals)
        {
            _highSys = limitVals.HighSys;
            _lowSys = limitVals.LowSys;
            _highDia = limitVals.HighDia;
            _lowDia = limitVals.LowDia;
            _highMean = limitVals.HighMean;
            _lowMean = limitVals.LowMean;

        }
        /// <summary>
        /// tjekker om grænseværdierne er overskredet
        /// </summary>
        /// <param name="calculated">Blodtryksværdierne, så de kan blive sammenlignet med grænseværdierne</param>
        /// <returns>en DTO af bools, der indikere om grænseværdierne er overskredet</returns>
        public DTO_ExceededVals LimitValExceeded(DTO_BP calculated)
        {
            _exceededVals = new DTO_ExceededVals(false, false, false, false, false, false);
            if (calculated.CalculatedSys >= _highSys)
            {
                _exceededVals.HighSys = true;
            }
            if (calculated.CalculatedSys <= _lowSys)
            {
                _exceededVals.LowSys = true;
            }
            if (calculated.CalculatedDia >= _highDia)
            {
                _exceededVals.HighDia = true;
            }
            if (calculated.CalculatedDia <= _lowDia)
            {
                _exceededVals.LowDia = true;
            }
            if (calculated.CalculatedMean >= _highMean)
            {
                _exceededVals.HighMean = true;
            }
            if (calculated.CalculatedMean <= _lowMean)
            {
                _exceededVals.LowMean = true;
            }

            return _exceededVals;
        }

    }
}
