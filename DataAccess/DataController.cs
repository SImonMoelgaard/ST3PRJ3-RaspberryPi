using System;
using System.Collections.Generic;
using System.Text;
using RaspberryPiCore;
using DTO_s;

namespace DataAccessLogic
{
    public class DataController
    {
        private readonly SendUI sendUi= new SendUI();

        public void ZeroAdjustRequest(double zeroAdjustMean)
        {
            sendUi.SendZeroAdjust(zeroAdjustMean);
        }

        public void SendMeanCal(double meanVal)
        {
            sendUi.SendMeanCalibration(meanVal);
        }
        public void SendZero(double zeroAdjustMean)
        {

        }

        public void SendCalculated(DTO_Calculated calculatedObj)
        {
            sendUi.SendCalculatedData(calculatedObj);
        }
    }
}
