//using System.Collections.Generic;
//using NUnit.Framework;
//using BusinessLogic;
//using DataAccessLogic;
//using DTO_s;


//namespace NUnitTestProject1
//{
//    public class Tests
//    {
//        [Test]
//        public void Test_Calibration_10_nozero()
//        {
//            Calibration uut =new Calibration();

//            List<double> list = new List<double>();
//            list.Add(20);
//            list.Add(0);
//            list.Add(11);
//            list.Add(9);


//            double calmean =uut.CalculateMeanVal(list, 0);


//            Assert.That(calmean, Is.EqualTo(9));
//        }

//        //[Test]
//        //public void Test_ínteractiontest_controller_listener()
//        //{
//        //    TestListener Test =new TestListener();
//        //   // DataController uut = new DataController(Test);

//        //    uut.StartUdpLimit();

//        //    Assert.That(Test.hasBeenInteractedWith, Is.True);


//        //}
        
//    }

//    public class TestListener : IListener
//    {
//        public void ReceiveSystemOn(bool systemOn)
//        {
//            throw new System.NotImplementedException();
//        }

//        public bool hasBeenInteractedWith;
//        public string Command { get; set; }
//        public DTO_LimitVals DtoLimit { get; set; }
//        public void ListenLimitValsPC()
//        {
//            hasBeenInteractedWith = true;
//        }

//        public void ListenCommandsPC()
//        {
//            throw new System.NotImplementedException();
//        }

//        public void AddToQueueCommand(string command)
//        {
//            throw new System.NotImplementedException();
//        }

//        public void AddToQueueDtoLimitVals(DTO_LimitVals dtoLimit)
//        {
//            throw new System.NotImplementedException();
//        }
//    }
//}