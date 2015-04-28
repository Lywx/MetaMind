namespace MetaMind.EngineTest.Parsers
{
    using MetaMind.Engine.Parsers.Grammars;

    using NUnit.Framework;

    using Sprache;

    [TestFixture]
    public class ConfigurationParserTest
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

            try
            {
                var parsed = ConfigurationFileGrammar.ConfigurationPairParser.Parse(input);

                Assert.AreEqual("a b", parsed.Key);
                Assert.AreEqual("b", parsed.Value);
            }
            catch (ParseException)
            {
            }
        }

        [Test]
        public void ConfigurationRhsTwo()
        {
            var input = "a = b c";

            try
            {
                var parsed = ConfigurationFileGrammar.ConfigurationPairParser.Parse(input);

                Assert.AreEqual("a", parsed.Key);
                Assert.AreEqual("b c", parsed.Value);
            }
            catch (ParseException)
            {
            }
        }

        [Test]
        public void ConfigurationComment()
        {
            var input = "\" a = b c";

            var parsed = ConfigurationFileGrammar.ConfigurationLineParser.Parse(input);

            Assert.AreEqual(null, parsed.Key);
            Assert.AreEqual(null, parsed.Value);
        }

        [Test]
        public void ConfigurationNonComment()
        {
            var input = "a = b c";

            var parsed = ConfigurationFileGrammar.ConfigurationLineParser.Parse(input);

            Assert.AreEqual("a", parsed.Key);
            Assert.AreEqual("b c", parsed.Value);
        }
    }
}