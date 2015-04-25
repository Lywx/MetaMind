namespace MetaMind.AcutanceTest.Parsers
{
    using MetaMind.Acutance.Parsers.Elements;
    using MetaMind.Acutance.Parsers.Grammars;

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

            Assert.AreEqual(TitleLevel.T2, parsed.Level);
            Assert.AreEqual("A Title", parsed.Name);
        }

        [TestMethod]
        public void KnowledgeATitleWithATimeTag()
        {
            var input = "## A Title [01:02:03]";

            var parsed = KnowledgeGrammar.TitleWithTimeTagParser.Parse(input);

            Assert.AreEqual(TitleLevel.T2, parsed.Level);
            Assert.AreEqual("A Title", parsed.Name);
            Assert.AreEqual(1, parsed.Time.Hours);
            Assert.AreEqual(2, parsed.Time.Minutes);
            Assert.AreEqual(3, parsed.Time.Seconds);
        }

        [TestMethod]
        public void KnowledgeAComplexTitleWithATimeTag()
        {
            var input = "## A Title That May Contains \",\" \".\" And Anythings. [01:02:03]";

            var parsed = KnowledgeGrammar.TitleWithTimeTagParser.Parse(input);

            Assert.AreEqual(TitleLevel.T2, parsed.Level);
            Assert.AreEqual("A Title That May Contains \",\" \".\" And Anythings.", parsed.Name);
            Assert.AreEqual(1, parsed.Time.Hours);
            Assert.AreEqual(2, parsed.Time.Minutes);
            Assert.AreEqual(3, parsed.Time.Seconds);
        }

        [TestMethod]
        public void KnowledgeATitleWithBracket()
        {
            var input = "###### A Title []";
            
            var parsed = KnowledgeGrammar.TitleWithBracketParser.Parse(input);

            Assert.AreEqual(TitleLevel.T6, parsed.Level);
            Assert.AreEqual("A Title", parsed.Name);
        }

        [TestMethod]
        public void KnowledgeANormalTitleWithoutTimeTag()
        {
            var input = "###### A Title";

            var parsed = KnowledgeGrammar.TitleParser.Parse(input);

            Assert.AreEqual(TitleLevel.T6   , parsed.Level);
            Assert.AreEqual(TitleType.Normal, parsed.Type);
            Assert.AreEqual("A Title"       , parsed.Name);
            Assert.AreEqual(0               , parsed.Time.Hours);
            Assert.AreEqual(0               , parsed.Time.Minutes);
            Assert.AreEqual(0               , parsed.Time.Seconds);
        }

        [TestMethod] public void KnowledgeAMomentTitleWithoutTimeTag()
        {
            var input  = "######! A Title";
            
            var parsed = KnowledgeGrammar.TitleParser.Parse(input); 

            Assert.AreEqual(TitleLevel.T6   , parsed.Level);
            Assert.AreEqual(TitleType.Normal, parsed.Type);
            Assert.AreEqual("A Title"       , parsed.Name);
            Assert.AreEqual(0               , parsed.Time.Hours);
            Assert.AreEqual(0               , parsed.Time.Minutes);
            Assert.AreEqual(0               , parsed.Time.Seconds);
        }

        [TestMethod] public void KnowledgeALinkTitleWithoutTimeTag()
        {
            var input  = "##> A Title";

            var parsed = KnowledgeGrammar.TitleParser.Parse(input); 

            Assert.AreEqual(TitleLevel.T2 , parsed.Level);
            Assert.AreEqual(TitleType.Link, parsed.Type);
            Assert.AreEqual("A Title"     , parsed.Name);
            Assert.AreEqual(0             , parsed.Time.Hours);
            Assert.AreEqual(0             , parsed.Time.Minutes);
            Assert.AreEqual(0             , parsed.Time.Seconds);
        }
    }
}
