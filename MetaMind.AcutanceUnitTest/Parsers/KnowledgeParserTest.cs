namespace MetaMind.AcutanceUnitTest.Parsers
{
    using MetaMind.Acutance.Parsers.Elements;
    using MetaMind.Acutance.Parsers.Grammers;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Sprache;

    [TestClass]
    public class KnowledgeParserTest
    {

        [TestMethod]
        public void KnowledgeATimeTagConciseFormat()
        {
            var input  = "20:30";

            var strategy = KnowledgeGrammar.TimeTagStrategyParser.Parse(input);
            var parsed   = strategy.Parse(input);

            Assert.AreEqual(0, parsed.Hours);
            Assert.AreEqual(20, parsed.Minutes);
            Assert.AreEqual(30, parsed.Seconds);
        }

        [TestMethod]
        public void KnowledgeATimeTagFullFormat()
        {
            var input  = "10:20:30";

            var strategy = KnowledgeGrammar.TimeTagStrategyParser.Parse(input);
            var parsed   = strategy.Parse(input);

            Assert.AreEqual(10, parsed.Hours);
            Assert.AreEqual(20, parsed.Minutes);
            Assert.AreEqual(30, parsed.Seconds);
        }

        [TestMethod]
        public void KnowledgeATitle()
        {
            var input = "## A Title";

            var parsed = KnowledgeGrammar.TitleParser.Parse(input);

            Assert.AreEqual(TitleLevel.Two, parsed.Level);
            Assert.AreEqual("A Title", parsed.Name);
        }

        [TestMethod]
        public void KnowledgeATitleWithATimeTag()
        {
            var input = "## A Title [01:02:03]";

            var parsed = KnowledgeGrammar.TitleWithTimeTagParser.Parse(input);

            Assert.AreEqual(TitleLevel.Two, parsed.Level);
            Assert.AreEqual("A Title", parsed.Name);
            Assert.AreEqual(1, parsed.Tag.Hours);
            Assert.AreEqual(2, parsed.Tag.Minutes);
            Assert.AreEqual(3, parsed.Tag.Seconds);
        }

        [TestMethod]
        public void KnowledgeATitleWithBracket()
        {
            var input = "###### A Title []";

            var parsed = KnowledgeGrammar.TitleWithBracketParser.Parse(input);

            Assert.AreEqual(TitleLevel.Six, parsed.Level);
            Assert.AreEqual("A Title", parsed.Name);
        }
        [TestMethod]
        public void KnowledgeATitleWithoutTimeTag()
        {
            Title parsed;

            var input = "###### A Title";

            try
            {
                parsed = KnowledgeGrammar.TitleWithBracketParser.Parse(input);
            }
            catch (ParseException)
            {
                parsed = KnowledgeGrammar.TitleParser.Parse(input);
            }

            Assert.AreEqual(TitleLevel.Six, parsed.Level);
            Assert.AreEqual("A Title", parsed.Name);
            Assert.AreEqual(0, parsed.Tag.Hours);
            Assert.AreEqual(0, parsed.Tag.Minutes);
            Assert.AreEqual(0, parsed.Tag.Seconds);
        }
    }
}
