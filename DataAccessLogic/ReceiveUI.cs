using System;
using System.Collections.Generic;
using System.Text;
using DTO_s;
using BusinessLogic;


namespace PresentationLogic
{
    public class ReceiveUI
    {
        private PresentationController presentationControllerObj=new PresentationController();
        /// <summary>
        /// en dto med grænseværdier
        /// </summary>
        private DTO_LimitVals LimitVals { get; set; }
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
            //mangler kode til UDP
            //LimitVals =  //Mangler kode til at få DTO'en ned
            presentationControllerObj.LimitValsEntered(LimitVals);
        }
        /// <summary>
        /// Hvis SP ønsker at mute alarmen, modtager RPi den request her, og sender videre til !!!!!!!!!!
        /// </summary>
        public void ReceiveMute()
        {

        }

        /// <summary>
        /// Den kalibrationsværdi, vi skal tage højde for i udregningen modtages her
        /// </summary>
        public void ReceiveCalibrationVal()
        {
            //UDP mangler
            presentationControllerObj.CalibrationVal(calibrationVal);
        }

        public void CalibrationRequest()
        {
            // Mangler kode omkring UDP
                presentationControllerObj.CalibrationRequest();
        }

        public void ZeroAdjustmentRequest()
        {
            //Der mangler her noget kode omkring UDP 
            presentationControllerObj.ZeroAdjustRequest();
        }

        public void StartRequest()
        {
            //UDP forbindelse mangler
            presentationControllerObj.StartMonitoringRequest();
        }

        public void StopMonitoringRequest()
        {
            //UDP mangler
            presentationControllerObj.StopMonitoring();
        }
    }
}
