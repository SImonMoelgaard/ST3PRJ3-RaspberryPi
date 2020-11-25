using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public class DataContainer
    {
        private double _measureData;
        public double GetMeasureData()
        {
            return _measureData;
        }

        public void SetMeasureData(double measureData)
        {
            this._measureData = measureData;
        }
    }



}
