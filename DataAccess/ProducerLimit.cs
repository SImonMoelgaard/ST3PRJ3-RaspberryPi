using System.Collections.Concurrent;
using System.Threading;

namespace DataAccessLogic
{
    public class ProducerLimit
    {
        private readonly BlockingCollection<DataContainerUdp> _dataQueuelimitLimit;
        private readonly BlockingCollection<DataContainerUdp> _dataQueueCommands;
        private readonly FakeListener _udpListener = new FakeListener();
        //private readonly UdpListener _udpListener = new UdpListener();
        private readonly bool _systemOn;


        public ProducerLimit(BlockingCollection<DataContainerUdp> dataQueuelimit, BlockingCollection<DataContainerUdp> dataQueueCommands, bool systemOn)
        {
            _dataQueuelimitLimit = dataQueuelimit;
            _dataQueueCommands = dataQueueCommands;
        }

        public void RunLimit()
        {
             
            while (_systemOn) 
            {
                DataContainerUdp reading = new DataContainerUdp();
                var dtoLimitVals = _udpListener.ListenLimitValsPC();
                reading.SetLimitVals(dtoLimitVals);
                _dataQueuelimitLimit.Add(reading);
                Thread.Sleep(10);
            }
            _dataQueuelimitLimit.CompleteAdding();
        }

        public void RunCommand()
        {
            while (_systemOn) //Denne bool skal sættes til true når programmet starter op, og sættes til false, når programmet lukkes ned
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