namespace MetaMind.EngineTest.Parsers
{
    using MetaMind.Engine.Parsers.Grammars;

    using NUnit.Framework;

    using Sprache;

    public class BasicParserTest
    {
        [Test]
        public void ABracketedText()
        {
            var input = "[Hello World]";

            var parsed = LineGrammar.BracketedTextParser.Parse(input);

            Assert.AreEqual("Hello World", parsed);
        }

        [Test]
        public void AWord()
        {
            // a word is a string of non-whitespace characters
            var input = "Hello";

            var parsed = LineGrammar.WordParser.Parse(input);

            Assert.AreEqual("Hello", parsed);
        }

        [Test]
        public void AWordInsideABracket()
        {
            var input = "[Hello]";

            var insideBracket = LineGrammar.BracketedTextParser.Parse(input);
            var parsed        = LineGrammar.WordParser         .Parse(insideBracket);

            Assert.AreEqual("Hello", parsed);
        }

        [Test]
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
