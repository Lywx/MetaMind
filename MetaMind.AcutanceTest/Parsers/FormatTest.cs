namespace MetaMind.AcutanceTest.Parsers
{
    using System.Collections.Generic;

    using MetaMind.Engine.Components.Fonts;

    using NUnit.Framework;

    [TestFixture]
    public class FormatTest
    {
        private static readonly string sanityComposed  = "              >               > Start";
        private static readonly string normalComposed  = "Acutance      > Check         > Start";
        private static readonly string normalReference = "Acutance      > Check         > Song";
        private static readonly string normalRaddled   = "                              > Start";

        [Test]
        public void ComposeTest()
        {
            var heads = new List<string> { "Acutance", "Check" };

            var actualComposed = FormatUtils.Compose(heads, 14, "", "> ", "Start", "", "");

            Assert.AreEqual(normalComposed, actualComposed);
        }

        [Test]
        public void ComposeSanityTest()
        {
            var heads = new List<string> { "", "" };

            var actualComposed = FormatUtils.Compose(heads, 14, "", "> ", "Start", "", "");

            Assert.AreEqual(sanityComposed, actualComposed);
        }

        [Test]
        public void DisintegrateTest()
        {
            var heads = FormatUtils.Disintegrate(normalComposed, 14, "", "> ", "", "");

            Assert.AreEqual("Acutance", heads[0]);
            Assert.AreEqual("Check"   , heads[1]);
            Assert.AreEqual("Start"   , heads[2]);
        }

        [Test]
        public void PaddleTest()
        {
            var actual = FormatUtils.Paddle(normalComposed, normalReference, 14, "", "> ", "", "");

            Assert.AreEqual(normalRaddled, actual);
        }
    }
}
