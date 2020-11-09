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

        public DTO_Calculated(int sys, int dia, int pulse, int batteryStatus, int alarmType)
        {
            sys = CalculatedSys;
            dia = CalculatedDia;
            pulse = CalculatedPulse;
            batteryStatus = BatteryStatus;
            alarmType = AlarmType;
        }
    }
}
