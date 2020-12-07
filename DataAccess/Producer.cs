using System.Collections.Concurrent;
using System.Dynamic;
using System.Threading;

namespace DataAccessLogic
{
    public class Producer
    {
        private readonly BlockingCollection<DataContainerUdp> _dataQueueLimit;
        private readonly BlockingCollection<DataContainerUdp> _dataQueueCommands;
        private IListener _udpListener = new FakeListener();
        public  bool SystemOn
        {
            get;
            set;
        }

 
        public Producer(BlockingCollection<DataContainerUdp> dataQueue, BlockingCollection<DataContainerUdp> dataQueueCommands, bool systemOn)
        {
            _dataQueueLimit = dataQueue;
            _dataQueueCommands = dataQueueCommands;
            SystemOn = true;
        }

        public void RunLimit() //den her bliver ikke kaldt, hvilket den bør
        {
             
            while (SystemOn) 
            {
                DataContainerUdp reading = new DataContainerUdp();
                var dtoLimitVals = _udpListener.ListenLimitValsPC();
                reading.SetLimitVals(dtoLimitVals);
                _dataQueueLimit.Add(reading);
                Thread.Sleep(10);
            }
            _dataQueueLimit.CompleteAdding();
        }

        public void RunCommand() //denne metode bliver ikke brugt, hvilket den bør
        {
            while (SystemOn) //Denne bool skal sættes til true når programmet starter op, og sættes til false, når programmet lukkes ned
            {
                DataContainerUdp reading = new DataContainerUdp();
                var command = _udpListener.ListenCommandsPC();
                reading.SetCommand(command);
                _dataQueueCommands.Add(reading);
                Thread.Sleep(10);

            }
            _dataQueueCommands.CompleteAdding();
        }
    }
}