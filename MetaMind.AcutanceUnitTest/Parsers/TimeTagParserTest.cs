namespace MetaMind.AcutanceUnitTest.Parsers
{
    using MetaMind.Acutance.Parsers.Grammers;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Sprache;

    [TestClass]
    public class TimeTagParserTest
    {
        [TestMethod]
        public void TimeTagFullFormat()
        {
            var input  = "10:20:30";

            var strategy = TimeTagGrammer.TimeTagStrategyParser.Parse(input);
            var parsed   = strategy.Parse(input);

            Assert.AreEqual(10, parsed.Hours);
            Assert.AreEqual(20, parsed.Minutes);
            Assert.AreEqual(30, parsed.Seconds);
        }

        [TestMethod]
        public void TimeTagConciseFormat()
        {
            var input  = "20:30";

            var strategy = TimeTagGrammer.TimeTagStrategyParser.Parse(input);
            var parsed   = strategy.Parse(input);

            Assert.AreEqual(0,  parsed.Hours);
            Assert.AreEqual(20, parsed.Minutes);
            Assert.AreEqual(30, parsed.Seconds);
        }
    }
}
