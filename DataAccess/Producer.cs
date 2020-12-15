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
        private readonly BlockingCollection<DataContainerUdp> _dataQueueLimit;
        private readonly BlockingCollection<DataContainerUdp> _dataQueueCommands;
        private readonly BlockingCollection<DataContainerMeasureVals> _dataQueueVals;
        private IListener _udpListener;
        private IBPData _adc = new ReceiveAdc();

        public string Command { get; set; }

        private bool _systemOn;



        public Producer(BlockingCollection<DataContainerUdp> dataQueueLimit, BlockingCollection<DataContainerUdp> dataQueueCommands, BlockingCollection<DataContainerMeasureVals> dataQueueVals)
        {
            _dataQueueLimit = dataQueueLimit;
            _dataQueueCommands = dataQueueCommands;
            _dataQueueVals = dataQueueVals;
            _udpListener = new UdpListener(dataQueueCommands);

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
                _udpListener.ListenLimitValsPC();
                DTO_LimitVals dtoLimitVals = _udpListener.DtoLimit;
                readingLimit.SetLimitVals(dtoLimitVals);
                _dataQueueLimit.Add(readingLimit);
                Thread.Sleep(10);
            }
            _dataQueueLimit.CompleteAdding();
        }

        public void newRunCommand(string command)
        {
            while (_systemOn)
            {
                DataContainerUdp readingCommand = new DataContainerUdp();
                //_udpListener.ListenCommandsPC();
                //string command = _udpListener.Command;

                readingCommand.SetCommand(Command);
                //Console.WriteLine("Producer " + command);
                _dataQueueCommands.Add(readingCommand);
                Thread.Sleep(10);

            }
            _dataQueueCommands.CompleteAdding();
        }

        public void RunCommand()
        {
            while (_systemOn)
            {
                DataContainerUdp readingCommand = new DataContainerUdp();
                _udpListener.ListenCommandsPC();
                string command = _udpListener.Command;
                readingCommand.SetCommand(command);
                //Console.WriteLine("Producer " + command);
                _dataQueueCommands.Add(readingCommand);
                Thread.Sleep(10);

            }
            _dataQueueCommands.CompleteAdding();
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