using System;
using System.Threading;
using NUnit.Framework;
using Autofac;

namespace PME.Test
{
    [TestFixture]
    public class Clocks
    {

        [Test]
        public void LiveClock()
        {
            var liveClock = TestApplicationState.Container.Resolve<DateTimeClock>();

            liveClock.Start();

            Thread.Sleep(50);

            Assert.Greater(liveClock.StartTimestamp, DateTime.MinValue);
            Assert.Greater(liveClock.Elapsed, TimeSpan.Zero);

            liveClock.Stop();

            Assert.AreEqual(liveClock.StartTimestamp, DateTime.MinValue);
            Assert.AreEqual(liveClock.Elapsed, TimeSpan.Zero);
        }

        [Test]
        public void TestClock()
        {
            var testClock = TestApplicationState.Container.Resolve<TestClock>();

            testClock.Start();

            Thread.Sleep(50);

            Assert.Greater(testClock.StartTimestamp, DateTime.MinValue);
            Assert.Greater(testClock.Elapsed, TimeSpan.Zero);

            var elapsed = testClock.Elapsed;

            Thread.Sleep(50);

            Assert.Greater(testClock.Elapsed, elapsed);

            testClock.Stop();

            Assert.AreEqual(testClock.StartTimestamp, DateTime.MinValue);
            Assert.AreEqual(testClock.Elapsed, TimeSpan.Zero);
        }

        [Test]
        public void TestClockCanMove()
        {
            var testClock = TestApplicationState.Container.Resolve<TestClock>();

            testClock.Start();

            var staticNow = DateTime.Parse("2/1/2008 14:02 PM");
            testClock.Now = staticNow;

            Thread.Sleep(-1);

            Assert.Greater(testClock.Now, staticNow);

            // Ensure that with a major date change that the Elapsed property doesn't
            // get to out of wack
            Assert.Less(testClock.Elapsed, TimeSpan.FromSeconds(5));
            
            testClock.Stop();
        }

    }
}

