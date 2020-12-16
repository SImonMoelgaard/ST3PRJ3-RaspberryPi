using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading;
using DTO_s;

namespace DataAccessLogic
{
    public class Producer
    {
       
        private readonly BlockingCollection<DataContainerMeasureVals> _dataQueueVals;
        private readonly IBPData _adc;
        private readonly bool _systemOn;

        public Producer(BlockingCollection<DataContainerMeasureVals> dataQueueVals)
        {  
            _dataQueueVals = dataQueueVals;
            _adc = new ReceiveAdc();
        }

   
        public void RunMeasure()
        {
            int count = 0;
            List<DTO_Raw> buffer = new List<DTO_Raw>(91);

            while (_systemOn)
            {
                var measureVal = _adc.Measure(); // blocking 20 ms 
                buffer.Add(measureVal); //værdierne her er i V og skal omregenes til mmHg(se evt convertBP i prossesing)
                //her vil vi stå til der er kommet 50 målinger
                count++;
                if (count == 91)
                {
                    DataContainerMeasureVals readingVals = new DataContainerMeasureVals();
                    readingVals._buffer = buffer;

                    _dataQueueVals.Add(readingVals);
                    buffer = new List<DTO_Raw>(91);
                    count = 0;
                }
            }
            _dataQueueVals.CompleteAdding();
        }
    }
}