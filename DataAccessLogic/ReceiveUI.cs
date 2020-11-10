using System;
using System.Collections.Generic;
using System.Text;
using DTO_s;

namespace DataAccessLogic
{
    public class ReceiveUI
    {
        /// <summary>
        /// en liste over grænseværdier TODO tilføj i hvilken rækkefølge grænseværdierne står i
        /// </summary>
        private List<int> limitVals;
        /// <summary>
        /// atribut, der indikere om SP har slået Mute til
        /// </summary>
        private bool mute;
        /// <summary>
        /// kalibrationsværdien, der er blevet udregnet på UI
        /// </summary>
        private double calibrationVal;
        
        /// <summary>
        /// Fra UI, modtager RPien en række grænseværdier, som bliver læst ind her, for så at blive sendt videre til Compare metoden i BuisnessLogic
        /// </summary>
        public void ReceiveLimitVals()
        {

        }
        /// <summary>
        /// Hvis SP ønsker at mute alarmen, modtager RPi den request her, og sender videre til !!!!!!!!!!
        /// </summary>
        public void ReceiveMute()
        {

        }

        /// <summary>
        /// Den kalibrationsværdi, vi skal tage højde for i udregningen
        /// </summary>
        public double ReceiveCalibrationVal()
        {
            return calibrationVal;
        }
    }
}
