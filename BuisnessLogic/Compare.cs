using System;
using System.Collections.Generic;
using System.Text;
using DTO_s;
using DataAccessLogic;

namespace BuisnessLogic
{
    public class Compare
    {
        /// <summary>
        /// indikere hvilken type alarm der bliver udløst TODO tilføj hvornår de forskellige alarmtyper bliver udløst
        /// </summary>
        private int alarmType;

        /// <summary>
        /// Sætter grænseværdierne til parametrerne 
        /// </summary>
        /// <param name="sys"> den systoliske (øvre) grænseværdi </param>
        /// <param name="meanBP"> grænseværdien (nedre) for middelblodtrykket </param>
        public void SetLimitVals(int sys, int meanBP)
        {

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
