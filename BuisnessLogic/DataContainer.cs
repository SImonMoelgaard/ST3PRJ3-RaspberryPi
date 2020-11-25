using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public class DataContainer
    {
        private double measureData;
        public double GetMeasureData()
        {
            return measureData;
        }

        public void SetMeasureData(double measureData)
        {
            this.measureData = measureData;
        }
    }



}
