using System.Runtime.InteropServices.WindowsRuntime;

namespace DataAccessLogic
{
    public class DataContainerMeasureVals
    {
        private double _measureVal;

        public double GetMeasureVal()
        {
            return _measureVal;
        }

        public void SetMeasureVal(double measureVal) => _measureVal = measureVal;
    }
}