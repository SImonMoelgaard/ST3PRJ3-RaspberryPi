using System;
using System.Collections.Generic;
using System.Text;

namespace DTO_s
{
    /// <summary>
    /// Denne klasse definere hvad et DTO_Raw er. DTO_Raw indeholder blodtryk og tid, som skal sendes sammen til UI, så det kan plottes på grafen
    /// </summary>
    public class DTO_Raw
    {

        public double mmHg
        {
            get;
            set;
        }

        public DateTime Tid
        {
            get;
            private set;
        }

        public DTO_Raw(double mmhg, DateTime tid)
        {
            mmHg = mmhg;
            Tid = tid;
        }
    }
}
