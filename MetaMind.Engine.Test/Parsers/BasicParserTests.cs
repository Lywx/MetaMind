namespace MetaMind.EngineTest.Parsers
{
    using Engine.Core.Services.Parser.Grammar;
    using NUnit.Framework;

    using Sprache;

    public class BasicParserTests
    {
        [Test]
        public void ABracketedText()
        {
            var input = "[Hello World]";

            var parsed = CommonGrammar.BracketParser.Parse(input);

            Assert.AreEqual("Hello World", parsed);
        }

        [Test]
        public void AWord()
        {
            // a word is a string of non-whitespace characters
            var input = "Hello";

            var parsed = CommonGrammar.WordParser.Parse(input);

            Assert.AreEqual("Hello", parsed);
        }

        [Test]
        public void AWordInsideABracket()
        {
            var input = "[Hello]";

            var insideBracket = CommonGrammar.BracketParser.Parse(input);
            var parsed        = CommonGrammar.WordParser         .Parse(insideBracket);

            Assert.AreEqual("Hello", parsed);
        }

        [Test]
        public void TwoWordInsideABracket()
        {
            var input = "[Hello World]";

            var insideBracket = CommonGrammar.BracketParser.Parse(input);
            var parsed        = CommonGrammar.SentenceParser     .Parse(insideBracket);

            Assert.AreEqual("Hello", parsed.Words[0]);
            Assert.AreEqual("World", parsed.Words[1]);
        }
    }
}
