using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading;
using DTO_s;

namespace DataAccessLogic
{
    public class Producer
    {
        private readonly BlockingCollection<DataContainerUdp> _dataQueueLimit;
        private readonly BlockingCollection<DataContainerUdp> _dataQueueCommands;
        private readonly BlockingCollection<DataContainerMeasureVals> _dataQueueVals;
        private IListener _udpListener = new FakeListener();
        private IBPData _adc = new ReadFromFile();

        private bool _systemOn;
      

 
        public Producer(BlockingCollection<DataContainerUdp> dataQueueLimit, BlockingCollection<DataContainerUdp> dataQueueCommands,BlockingCollection<DataContainerMeasureVals> dataQueueVals)
        {
            _dataQueueLimit = dataQueueLimit;
            _dataQueueCommands = dataQueueCommands;
            _dataQueueVals = dataQueueVals;
           
        }

        public void ReceiveSystemOn(bool systemOn)
        {
            _systemOn = systemOn;

        }
        public void RunLimit() 
        {
            while (_systemOn) 
            {
                DataContainerUdp readingLimit = new DataContainerUdp();
                DTO_LimitVals dtoLimitVals = _udpListener.ListenLimitValsPC();
                readingLimit.SetLimitVals(dtoLimitVals);
                _dataQueueLimit.Add(readingLimit);
                Thread.Sleep(10);
            }
            _dataQueueLimit.CompleteAdding();
        }

        public void RunCommand() 
        {
            while (_systemOn) 
            {
                DataContainerUdp readingCommand = new DataContainerUdp();
                var command = _udpListener.ListenCommandsPC();
                readingCommand.SetCommand(command);
                _dataQueueCommands.Add(readingCommand);
                Thread.Sleep(10);

            }
            _dataQueueCommands.CompleteAdding();
        }
        public void RunMeasure(DTO_Raw raw)
        {
            int count = 0;
            List<double> buffer = new List<double>(45);

            while (_systemOn)
            {
                
                //var measureVal = raw;
                var measureVal = _adc.Measure(); // blocking 20 ms 
                buffer.Add(measureVal); //værdierne her er i V og skal omregenes til mmHg(se evt convertBP i prossesing)
                //her vil vi stå til der er kommet 50 målinger
                
                if (count == 45)
                {
                    DataContainerMeasureVals readingVals = new DataContainerMeasureVals();
                    readingVals._buffer = buffer;
                    
                    _dataQueueVals.Add(readingVals);
                    buffer = new List<double>(45);
                    count = 0;
                }

                count++;
            }
            _dataQueueVals.CompleteAdding();


            //List<double> _bpList = new List<double>();

            //while (_systemOn)
            //{
            //    DataContainerMeasureVals dataContainer = new DataContainerMeasureVals();
            //    foreach (var BP in rawList)
            //    {
            //        dataContainer.SetMeasureVal(BP.mmHg);
            //        _dataQueueVals.Add(dataContainer);
            //        Thread.Sleep(10); // Ved egentlig ikke om den skal "sleep" 
                    
            //    }
            //}
            //_dataQueueVals.CompleteAdding();
        }
    }
}