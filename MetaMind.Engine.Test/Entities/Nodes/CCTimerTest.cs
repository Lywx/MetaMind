using System;

namespace MetaMind.EngineTest.Entities.Nodes
{
    using Engine.Core.Entity.Node;
    using Engine.Core.Entity.Node.Model;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class CCTimerTest
    {
        [Test]
        public void TestDelay()
        {
            var scheduler = new Mock<ICCScheduler>();
            var selector = new Action<float>(dt => {});
            var target = new MMNode();

            scheduler.Setup(foo => foo.Unschedule(selector, target));

            var timer = new CCTimer(scheduler, target, selector, 0, 0, 0);
        }
    }
}