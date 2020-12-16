using System;
using System.Collections.Generic;
using System.Text;
using DTO_s;

namespace DataAccessLogic
{
    /// <summary>
    /// interface til senderen
    /// </summary>
    public interface ISender
    {
        /// <summary>
        /// sender calculated bestående af sys, dia, middel, puls, batteristatus og om overskridelser af grænseværdier er sket
        /// </summary>
        /// <param name="dtoCalculated">dto calculated</param>
        void SendDTO_Calculated(DTO_Calculated dtoCalculated);
       /// <summary>
       /// sender liste med dto raw bestående af målinger over et halvt sekund i mmHg med dertilhørende tidspunkt
       /// </summary>
       /// <param name="dtoRaw"></param>
        void SendDTO_Raw(List<DTO_Raw> dtoRaw);
       /// <summary>
       /// sender en double
       /// </summary>
       /// <param name="meanVal">enten zeroval eller en kalibreringsværdi</param>
        void SendDouble(double meanVal);
    
    }
}