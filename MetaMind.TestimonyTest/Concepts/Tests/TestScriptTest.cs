namespace MetaMind.TestimonyTest.Concepts.Tests
{
    using System;
    using NUnit.Framework;

    using Testimony.Scripting;

    [TestFixture]
    public class TestScriptTest
    {
        private FsiSession session;

        [SetUp]
        public void Setup()
        {
            this.session = new FsiSession();
            this.session.Out.Clear();
        }

        [TearDown]
        public void TearDown()
        {
            this.session.Out.Clear();
        }

        [Test]
        public void RunScript()
        {
            var script = new Script(@"Resources\Test_Hello_World.fsx");
            script.Run(this.session);

            Console.WriteLine(this.session.Out.ToString());
        }
    }
}