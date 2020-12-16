using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using DTO_s;

namespace DataAccessLogic

{
    /// <summary>
    /// datakø til blodtryksværdier
    /// </summary>
    public class DataContainerMeasureVals
    {
       /// <summary>
       /// property til listen af blodtryksmålinger med dertilhørende blodtryk
       /// </summary>
        public List<DTO_Raw> _buffer { get; set; }

      
    }
}