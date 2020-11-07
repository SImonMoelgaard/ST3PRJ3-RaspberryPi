using System;
using System.Collections.Generic;
using System.Text;

namespace DTO_s
{
    public class DTO_Raw
    {

        //public DateTime start_tidspunkt { get; set; }
        //public double[] raa_data { get; set; }

        //public DTO_Raw(DateTime startTidspunkt, double[] raaData)
        //{

        //    this.start_tidspunkt = DateTime.Now;
        //    this.raa_data = raaData;

        //}
        public double mmHg
        {
            get;
            private set;
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
