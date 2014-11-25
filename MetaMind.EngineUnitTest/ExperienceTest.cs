// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExperienceTest.cs" company="UESTC">
//   Copyright (c) 2014 Lin Wuxiang
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.EngineUnitTest
{
    using System;

    using MetaMind.Engine.Concepts;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ExperienceTest
    {
        [TestMethod]
        public void DurationTestCaseAddition()
        {
            var exp1 = new Experience(
                new DateTime(2014, 7, 20, 0, 0, 0), 
                endTime: new DateTime(2014, 7, 21, 0, 0, 0), 
                certainDuration: TimeSpan.FromDays(1));

            var exp2 = new Experience(
                new DateTime(2014, 7, 21, 0, 0, 0), 
                endTime: new DateTime(2014, 7, 22, 0, 0, 0), 
                certainDuration: TimeSpan.FromDays(1));

            var exp = exp1 + exp2;

            Assert.AreEqual(exp.Duration, TimeSpan.FromDays(2));
            Assert.AreEqual(exp.HistoricalStartTime, new DateTime(2014, 7, 20, 0, 0, 0));
            Assert.AreEqual(exp.EndTime, new DateTime(2014, 7, 22, 0, 0, 0));
        }

        [TestMethod]
        public void DurationTestCaseDuration()
        {
            var exp = new Experience(
                new DateTime(2014, 7, 21, 0, 0, 0), 
                endTime: new DateTime(2014, 7, 22, 0, 0, 0), 
                certainDuration: TimeSpan.FromDays(1));

            Assert.AreEqual(exp.Duration, TimeSpan.FromDays(1));
        }

        [TestMethod]
        public void DurationTestCaseSubtraction()
        {
            var exp1 = new Experience(
                new DateTime(2014, 7, 20, 0, 0, 0), 
                endTime: new DateTime(2014, 7, 21, 0, 0, 0), 
                certainDuration: TimeSpan.FromDays(1));

            var exp2 = new Experience(
                new DateTime(2014, 7, 20, 0, 0, 0), 
                endTime: new DateTime(2014, 7, 22, 0, 0, 0), 
                certainDuration: TimeSpan.FromDays(2));

            var exp = exp2 - exp1;

            // should be zero ideally, but it should pass if it is nearly zero
            Assert.AreEqual(exp.Duration, TimeSpan.FromDays(1));
        }
    }
}