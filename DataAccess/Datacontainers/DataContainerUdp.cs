using System;
using System.Collections.Generic;
using System.Text;
using DTO_s;

namespace DataAccessLogic
{
    public class DataContainerUdp
    {

        private string _command;

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