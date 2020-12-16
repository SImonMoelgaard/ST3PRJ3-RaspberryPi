using System;
using System.Collections.Generic;
using System.Text;
using DTO_s;

namespace DataAccessLogic
{
    /// <summary>
    /// datakø til det der kommer fra PCen
    /// </summary>
    public class DataContainerUdp
    {
        /// <summary>
        /// property til komandoer
        /// </summary>
        private string _command;
        /// <summary>
        /// propperty til limitvals
        /// </summary>
        private DTO_LimitVals _limitVals;

        public DTO_LimitVals GetLimitVals()
        {
            return _limitVals;
        }

        public void SetLimitVals(DTO_LimitVals limitVals)
        {
            _limitVals = limitVals;
        }
        public string GetCommand()
        {
            return _command;
        }

        public void SetCommand(string command)
        {
            _command = command;
        }
    }
}