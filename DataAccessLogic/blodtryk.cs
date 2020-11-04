using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLogic
{
    public class blodtryk
    {
        public double mmHg
        {
            get;
            private set;
        }

        public string Tid
        {
            get;
            private set;
        }

        public blodtryk(double mmhg, string tid)
        {
            mmHg = mmhg;
            Tid = tid;
        }

    }
}
