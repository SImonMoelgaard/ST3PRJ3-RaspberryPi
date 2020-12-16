using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading;
using DTO_s;

namespace DataAccessLogic
{
    /// <summary>
    /// producer til målinger
    /// </summary>
    public class Producer
    {
       
        private readonly BlockingCollection<DataContainerMeasureVals> _dataQueueVals;
        private readonly IBPData _adc;
        public bool StartMonitoring { get; set; }
        /// <summary>
        /// constuctor
        /// </summary>
        /// <param name="dataQueueVals"></param>
        public Producer(BlockingCollection<DataContainerMeasureVals> dataQueueVals)
        {  
            _dataQueueVals = dataQueueVals;
            _adc = new ReceiveAdc();
        }

        /// <summary>
        /// producere en liste af dtomålinge, hentet fra ADC'en
        /// </summary>
        public void RunMeasure()
        {
            int count = 0;
            List<DTO_Raw> buffer = new List<DTO_Raw>(91);

            while (StartMonitoring)
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