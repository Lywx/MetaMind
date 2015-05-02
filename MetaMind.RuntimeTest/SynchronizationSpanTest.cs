// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SynchronizationSpanTest.cs" company="UESTC">
//   Copyright (c) 2014 Lin Wuxiang
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.RuntimeTest
{
    using System;

    using MetaMind.Runtime.Concepts.Synchronizations;

    using NUnit.Framework;

    [TestFixture]
    public class SynchronizationSpanTest
    {
        [Test]
        public void Addition()
        {
            var exp1 = new SynchronizationSpan(
                firstStartTime: new DateTime(2014, 7, 20, 0, 0, 0),
                recentEndTime: new DateTime(2014, 7, 21, 0, 0, 0),
                certainDuration: TimeSpan.FromDays(1));

            var exp2 = new SynchronizationSpan(
                firstStartTime: new DateTime(2014, 7, 21, 0, 0, 0),
                recentEndTime: new DateTime(2014, 7, 22, 0, 0, 0),
                certainDuration: TimeSpan.FromDays(1));

            var exp = exp1 + exp2;

            Assert.AreEqual(exp.Duration, TimeSpan.FromDays(2));
            Assert.AreEqual(exp.FirstStartTime, new DateTime(2014, 7, 20, 0, 0, 0));
            Assert.AreEqual(exp.RecentEndTime, new DateTime(2014, 7, 22, 0, 0, 0));
        }

        [Test]
        public void Duration()
        {
            var exp = new SynchronizationSpan(
                firstStartTime: new DateTime(2014, 7, 21, 0, 0, 0),
                recentEndTime: new DateTime(2014, 7, 22, 0, 0, 0),
                certainDuration: TimeSpan.FromDays(1));

            Assert.AreEqual(exp.Duration, TimeSpan.FromDays(1));
        }

        [Test]
        public void Subtraction()
        {
            var exp1 = new SynchronizationSpan(
                firstStartTime: new DateTime(2014, 7, 20, 0, 0, 0),
                recentEndTime: new DateTime(2014, 7, 21, 0, 0, 0),
                certainDuration: TimeSpan.FromDays(1));

            var exp2 = new SynchronizationSpan(
                firstStartTime: new DateTime(2014, 7, 20, 0, 0, 0),
                recentEndTime: new DateTime(2014, 7, 22, 0, 0, 0),
                certainDuration: TimeSpan.FromDays(2));

            var exp = exp2 - exp1;

            // should be zero ideally, but it should pass if it is nearly zero
            Assert.AreEqual(exp.Duration, TimeSpan.FromDays(1));
        }
    }
}