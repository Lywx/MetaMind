namespace MetaMind.EngineTest.Parsers
{
    using Engine.Parsers.Grammars;

    using NUnit.Framework;

    using Sprache;

    [TestFixture]
    public class ConfigurationParserTests
    {
        [Test]
        public void ConfigurationCompact()
        {
            var input = "a=b";

            var parsed = ConfigurationFileGrammar.ConfigurationPairParser.Parse(input);

            Assert.AreEqual("a", parsed.Key);
            Assert.AreEqual("b", parsed.Value);
        }

        [Test]
        public void ConfigurationConcise()
        {
            var input = "a = b";

            var parsed = ConfigurationFileGrammar.ConfigurationPairParser.Parse(input);

            Assert.AreEqual("a", parsed.Key);
            Assert.AreEqual("b", parsed.Value);
        }

        [Test]
        public void ConfigurationLong()
        {
            var input = "aaa = bbb";

            var parsed = ConfigurationFileGrammar.ConfigurationPairParser.Parse(input);

            Assert.AreEqual("aaa", parsed.Key);
            Assert.AreEqual("bbb", parsed.Value);
        }

        [Test]
        public void ConfigurationLhsTwo()
        {
            var input = "a b = b";

            var parsed = ConfigurationFileGrammar.ConfigurationPairParser.Parse(input);

            Assert.AreEqual("a b", parsed.Key);
            Assert.AreEqual("b", parsed.Value);
        }

        [Test]
        public void ConfigurationRhsTwo()
        {
            var input = "a = b c";

            var parsed = ConfigurationFileGrammar.ConfigurationPairParser.Parse(input);

            Assert.AreEqual("a", parsed.Key);
            Assert.AreEqual("b c", parsed.Value);
        }

        [Test]
        public void ConfigurationCommentOnly()
        {
            var input = "\" a = b c";

            var parsed = ConfigurationFileGrammar.ConfigurationLineParser.Parse(input);

            Assert.AreEqual(null, parsed.Key);
            Assert.AreEqual(null, parsed.Value);
        }

        [Test]
        public void ConfigurationCommentSubfix()
        {
            var input = "a = b c \" comment";

            var parsed = ConfigurationFileGrammar.ConfigurationLineParser.Parse(input);

            Assert.AreEqual("a", parsed.Key);
            Assert.AreEqual("b c", parsed.Value);
        }
    }
}