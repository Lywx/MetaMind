namespace MetaMind.EngineUnitTest.Parsers
{
    using MetaMind.Engine.Parsers.Grammars;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Sprache;

    public class BasicParserTest
    {
        [TestMethod]
        public void ABracketedText()
        {
            var input = "[Hello World]";

            var parsed = LineGrammar.BracketedTextParser.Parse(input);

            Assert.AreEqual("Hello World", parsed);
        }

        [TestMethod]
        public void AWord()
        {
            // a word is a string of non-whitespace characters
            var input = "Hello";

            var parsed = LineGrammar.WordParser.Parse(input);

            Assert.AreEqual("Hello", parsed);
        }

        [TestMethod]
        public void AWordInsideABracket()
        {
            var input = "[Hello]";

            var insideBracket = LineGrammar.BracketedTextParser.Parse(input);
            var parsed        = LineGrammar.WordParser         .Parse(insideBracket);

            Assert.AreEqual("Hello", parsed);
        }

        [TestMethod]
        public void TwoWordInsideABracket()
        {
            var input = "[Hello World]";

            var insideBracket = LineGrammar.BracketedTextParser.Parse(input);
            var parsed        = LineGrammar.SentenceParser     .Parse(insideBracket);

            Assert.AreEqual("Hello", parsed.Words[0]);
            Assert.AreEqual("World", parsed.Words[1]);
        }
    }
}
