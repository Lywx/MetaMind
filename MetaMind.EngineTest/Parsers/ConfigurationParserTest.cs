namespace MetaMind.EngineTest.Parsers
{
    using MetaMind.Engine.Parsers.Grammars;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Sprache;

    [TestClass]
    public class ConfigurationParserTest
    {
        [TestMethod]
        public void ConfigurationCompact()
        {
            var input = "a=b";

            var parsed = ConfigurationFileGrammar.ConfigurationPairParser.Parse(input);

            Assert.AreEqual("a", parsed.Key);
            Assert.AreEqual("b", parsed.Value);
        }

        [TestMethod]
        public void ConfigurationConcise()
        {
            var input = "a = b";

            var parsed = ConfigurationFileGrammar.ConfigurationPairParser.Parse(input);

            Assert.AreEqual("a", parsed.Key);
            Assert.AreEqual("b", parsed.Value);
        }

        [TestMethod]
        public void ConfigurationLong()
        {
            var input = "aaa = bbb";

            var parsed = ConfigurationFileGrammar.ConfigurationPairParser.Parse(input);

            Assert.AreEqual("aaa", parsed.Key);
            Assert.AreEqual("bbb", parsed.Value);
        }

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
        public void ConfigurationComment()
        {
            var input = "\" a = b c";

            var parsed = ConfigurationFileGrammar.ConfigurationLineParser.Parse(input);

            Assert.AreEqual(null, parsed.Key);
            Assert.AreEqual(null, parsed.Value);
        }

        [TestMethod]
        public void ConfigurationNonComment()
        {
            var input = "a = b c";

            var parsed = ConfigurationFileGrammar.ConfigurationLineParser.Parse(input);

            Assert.AreEqual("a", parsed.Key);
            Assert.AreEqual("b c", parsed.Value);
        }
    }
}