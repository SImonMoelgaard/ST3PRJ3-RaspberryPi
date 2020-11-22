using System;

namespace DTO_s
{ /// <summary>
/// Denne Klasse opretter et DTO objekt med Sys, Dia, Puls, Batteristatus og alarmtype, så 
/// </summary>
    public class DTO_Calculated
    {
        private int CalculatedSys
        {
            get;
            set;
        }

        private int CalculatedDia {
            get;
            set;
        }
        private int CalculatedMean
        {
            get;
            set;
        }
        private int CalculatedPulse {
            get;
            set;
        }
        private int BatteryStatus
        {
            get;
            set;
        }
        private int AlarmType
        {
            get;
            set;
        }

        public DTO_Calculated(int sys, int dia,int mean, int pulse, int batteryStatus, int alarmType)
        {
            CalculatedSys = sys;
            CalculatedDia =dia;
            CalculatedMean = mean;
            CalculatedPulse=CalculatedPulse;
            BatteryStatus=batteryStatus;
            AlarmType=alarmType;
        }
    }
}
