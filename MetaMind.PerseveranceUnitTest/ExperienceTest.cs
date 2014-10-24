using System;
using MetaMind.Perseverance.Concepts.TaskEntries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MetaMind.PerseveranceUnitTest
{
    [TestClass]
    public class ExperienceTest
    {
        [TestMethod]
        public void DurationTestCase_Addition()
        {
            var exp1 = new Experience( historicalStartTime: new DateTime( 2014, 7, 20, 0, 0, 0 ),
                endTime: new DateTime( 2014, 7, 21, 0, 0, 0 ),
                certainDuration: TimeSpan.FromDays( 1 ) );

            var exp2 = new Experience( historicalStartTime: new DateTime( 2014, 7, 21, 0, 0, 0 ),
                endTime: new DateTime( 2014, 7, 22, 0, 0, 0 ),
                certainDuration: TimeSpan.FromDays( 1 ) );

            var exp = exp1 + exp2;

            Assert.AreEqual( exp.Duration, TimeSpan.FromDays( 2 ) );
            Assert.AreEqual( exp.HistoricalStartTime, new DateTime( 2014, 7, 20, 0, 0, 0 ) );
            Assert.AreEqual( exp.EndTime, new DateTime( 2014, 7, 22, 0, 0, 0 ) );
        }

        [TestMethod]
        public void DurationTestCase_Duration()
        {
            var exp = new Experience( historicalStartTime: new DateTime( 2014, 7, 21, 0, 0, 0 ),
                endTime: new DateTime( 2014, 7, 22, 0, 0, 0 ),
                certainDuration: TimeSpan.FromDays( 1 ) );

            Assert.AreEqual( exp.Duration, TimeSpan.FromDays( 1 ) );
        }

        [TestMethod]
        public void DurationTestCase_Subtraction()
        {
            var exp1 = new Experience( historicalStartTime: new DateTime( 2014, 7, 20, 0, 0, 0 ),
                endTime: new DateTime( 2014, 7, 21, 0, 0, 0 ),
                certainDuration: TimeSpan.FromDays( 1 ) );

            var exp2 = new Experience( historicalStartTime: new DateTime( 2014, 7, 20, 0, 0, 0 ),
                endTime: new DateTime( 2014, 7, 22, 0, 0, 0 ),
                certainDuration: TimeSpan.FromDays( 2 ) );

            var exp = exp2 - exp1;

            // should be zero ideally, but it should pass if it is nearly zero
            Assert.AreEqual( exp.Duration, TimeSpan.FromDays( 1 ) );
        }
    }
}