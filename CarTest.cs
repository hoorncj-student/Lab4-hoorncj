using System;
using NUnit.Framework;
using Expedia;
using Rhino.Mocks;

namespace ExpediaTest
{
	[TestFixture()]
	public class CarTest
	{	
		private Car targetCar;
		private MockRepository mocks;
		
		[SetUp()]
		public void SetUp()
		{
			targetCar = new Car(5);
			mocks = new MockRepository();
		}
		
		[Test()]
		public void TestThatCarInitializes()
		{
			Assert.IsNotNull(targetCar);
		}	
		
		[Test()]
		public void TestThatCarHasCorrectBasePriceForFiveDays()
		{
			Assert.AreEqual(50, targetCar.getBasePrice()	);
		}
		
		[Test()]
		public void TestThatCarHasCorrectBasePriceForTenDays()
		{
            var target = new Car(10);
			Assert.AreEqual(80, target.getBasePrice());	
		}
		
		[Test()]
		public void TestThatCarHasCorrectBasePriceForSevenDays()
		{
			var target = new Car(7);
			Assert.AreEqual(10*7*.8, target.getBasePrice());
		}
		
		[Test()]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestThatCarThrowsOnBadLength()
		{
			new Car(-5);
		}

        [Test()]
        public void TestThatCarDoesGetCarLocation()
        {
            IDatabase mockDatabase = mocks.Stub<IDatabase>();
            Car fastCar = new Car(2);

            using (mocks.Record())
            {
                mockDatabase.getCarLocation(1);
                LastCall.Return("Lot 1");
                mockDatabase.getCarLocation(10);
                LastCall.Return("Lot 10");
                mockDatabase.getCarLocation(3);
                LastCall.Return("Lot 3");
            }
            fastCar.Database = mockDatabase;
            string loc;
            loc = fastCar.getCarLocation(1);
            Assert.AreEqual("Lot 1", loc);
            loc = fastCar.getCarLocation(3);
            Assert.AreEqual("Lot 3", loc);
            loc = fastCar.getCarLocation(10);
            Assert.AreEqual("Lot 10", loc);
        }

        [Test()]
        public void TestThatCarDoesMilage()
        {
            IDatabase mockDatabase = mocks.Stub<IDatabase>();
            Car fastCar = new Car(2);
            mockDatabase.Miles = 32000;
            fastCar.Database = mockDatabase;
            Assert.AreEqual(32000, fastCar.Mileage);
        }

        [Test()]
        public void TestForBMWFromObjectMother()
        {
            Car beamer = ObjectMother.BMW();
            Assert.AreEqual(beamer.getBasePrice(), 80);
        }

	}
}
