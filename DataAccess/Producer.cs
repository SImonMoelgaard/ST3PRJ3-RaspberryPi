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

            while (_systemOn)
            {
                DataContainerMeasureVals readingVals= new DataContainerMeasureVals();
                var measureVal = raw;
                readingVals.SetMeasureVal(measureVal.mmHg);
                _dataQueueVals.Add(readingVals);
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