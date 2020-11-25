using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLogic
{
    public class DataContainerUdp
    {

        private string _command;
        private double _calVal;
        private double _zeroVal;
        public string GetCommand()
        {
            return _command;
        }

        public void SetCommand(string command)
        {
            _command = command;
        }

        public double GetCalVal()
        {
            return _calVal;
        }

        public void SetCalVal(double calVal)
        {
            _calVal = calVal;
        }

        public double GetZeroVal()
        {
            return _zeroVal;
        }

        public void SetZeroVal(double zeroVal)
        {
            _zeroVal = zeroVal;
        }





    }



}
