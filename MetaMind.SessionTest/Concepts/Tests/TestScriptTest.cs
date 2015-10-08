namespace MetaMind.UnityTest.Concepts.Tests
{
    using System;
    using Engine.Services.Script.FSharp;
    using NUnit.Framework;

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
            var script = new FsScript(@"Resources\Test_Hello_World.fsx");
            script.Run(this.session);

            Console.WriteLine(this.session.Out.ToString());
        }
    }
}