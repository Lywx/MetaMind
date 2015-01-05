using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaMind.AcutanceUnitTest.Parsers
{
    using MetaMind.Acutance.Parsers.Grammers;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Sprache;

    public class BasicParserTest
    {
        [TestMethod]
        public void ABracketedText()
        {
            var input = "[Hello World]";

            var parsed = BasicGrammar.BracketedTextParser.Parse(input);

            Assert.AreEqual("Hello World", parsed);
        }

        [TestMethod]
        public void AWord()
        {
            // a word is a string of non-whitespace characters
            var input = "Hello";

            var parsed = BasicGrammar.WordParser.Parse(input);

            Assert.AreEqual("Hello", parsed);
        }

        [TestMethod]
        public void AWordInsideABracket()
        {
            var input = "[Hello]";

            var insideBracket = BasicGrammar.BracketedTextParser.Parse(input);
            var parsed        = BasicGrammar.WordParser         .Parse(insideBracket);

            Assert.AreEqual("Hello", parsed);
        }

        [TestMethod]
        public void TwoWordInsideABracket()
        {
            var input = "[Hello World]";

            var insideBracket = BasicGrammar.BracketedTextParser.Parse(input);
            var parsed        = BasicGrammar.SentenceParser     .Parse(insideBracket);

            Assert.AreEqual("Hello", parsed.Words[0]);
            Assert.AreEqual("World", parsed.Words[1]);
        }
    }
}
