using NUnit.Framework;
using BusinessLogic;
using System.Collections.Generic;

namespace processing.NUnit.Test
{
    public class ProcessingUnitTest
    {
        public List<double> bpList = new List<double>();
        [SetUp]
        public void Setup()
        {
            
            bpList.Add(69.5);
            bpList.Add(120.7);
            bpList.Add(89.3);
            bpList.Add(113.4);
            bpList.Add(75.3);
            bpList.Add(103.6);
            bpList.Add(66.7);
            bpList.Add(114.6);
            bpList.Add(80);
        }

        [Test]
        public void CalculateMean_CalculateFromListReturns93()
        {
            var uut = new Processing();
            Assert.That(uut.CalculateMean(bpList), Is.EqualTo(93));
        }

        [Test]
        public void CalculateSys_CalculateFromListReturns121()
        {
            var uut = new Processing();
            Assert.That(uut.CalculateSys(bpList), Is.EqualTo(121));            
        }

        [Test]
        public void CalculateDia_CalculateFromListReturns67()
        {
            var uut = new Processing();
            Assert.That(uut.CalculateDia(bpList), Is.EqualTo(67));
        }

       

        [Test]
        public void CalculatePulse_CalculateFromListReturns80()
        {
            int mean = 93;
            var uut = new Processing();
            Assert.That(uut.CalculatePulse(bpList, mean), Is.EqualTo(80));
            //Tror denne fejler fordi vi tæller antal steder vi har middelværdien, men den har vi nødvendigvis ikke i listen. 
        }

    }

    public class CalibrationUnitTest
    {
        private List<double> calVals;
        private int zeroPoint;
        [SetUp]
        public void SetUp()
        {
            calVals = new List<double>();
            calVals.Add(34.7);
            calVals.Add(36.3);
            calVals.Add(33.8);
            calVals.Add(32);
            calVals.Add(35.6);
            calVals.Add(36.2);
            zeroPoint = 15;
        }

        [Test]
        public void CalculateMeanVal_CalculateFromListReturns20()
        {
           
            var uut = new Calibration();
            Assert.That(uut.CalculateMeanVal(calVals, zeroPoint), Is.EqualTo(19.797).Within(0.1));
        }
    }
    public class ZeroAdjustmentUnitTest
    {
        private List<double> zeroAdjustVals;
        [SetUp]
        public void SetUp()
        {
            zeroAdjustVals = new List<double>();
            zeroAdjustVals.Add(14.7);
            zeroAdjustVals.Add(15.2);
            zeroAdjustVals.Add(16.8);
            zeroAdjustVals.Add(15);
            zeroAdjustVals.Add(14.6);
            zeroAdjustVals.Add(13.2);
        }

        [Test]
        public void CalculateZeroAdjust_FromListReturns15()
        {
            var uut = new ZeroAdjustment();
            Assert.That(uut.CalculateZeroAdjustMean(zeroAdjustVals), Is.EqualTo(14.917).Within(0.1));
        }
    }
}