using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading;
using DTO_s;

namespace DataAccessLogic
{
    public class Producer
    {
        private readonly BlockingCollection<DataContainerUdp> _dataQueueLimitLimit;
        private readonly BlockingCollection<DataContainerUdp> _dataQueueCommands;
        private readonly BlockingCollection<DataContainerMeasureVals> _dataQueueVals;
        private IListener _udpListener = new FakeListener();

        private bool _systemOn;
      

 
        public Producer(BlockingCollection<DataContainerUdp> dataQueueLimit, BlockingCollection<DataContainerUdp> dataQueueCommands,BlockingCollection<DataContainerMeasureVals> dataQueueVals)
        {
            _dataQueueLimitLimit = dataQueueLimit;
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
                DataContainerUdp reading = new DataContainerUdp();
                var dtoLimitVals = _udpListener.ListenLimitValsPC();
                reading.SetLimitVals(dtoLimitVals);
                _dataQueueLimitLimit.Add(reading); //exeption her 
                Thread.Sleep(10);
            }
            _dataQueueLimitLimit.CompleteAdding();
        }

        public void RunCommand() 
        {
            while (_systemOn) 
            {
                DataContainerUdp reading = new DataContainerUdp();
                var command = _udpListener.ListenCommandsPC();
                reading.SetCommand(command);
                _dataQueueCommands.Add(reading);
                Thread.Sleep(10);

            }
            _dataQueueCommands.CompleteAdding();
        }
        public void AddToQueue(List<DTO_Raw> rawList)
        {
            List<double> _bpList = new List<double>();

            while (_systemOn)
            {
                DataContainerMeasureVals dataContainer = new DataContainerMeasureVals();
                foreach (var BP in rawList)
                {
                    dataContainer.SetMeasureVal(BP.mmHg);
                    _dataQueueVals.Add(dataContainer);
                    Thread.Sleep(10); // Ved egentlig ikke om den skal "sleep" 
                    
                }
            }
            _dataQueueVals.CompleteAdding();
        }
    }
}