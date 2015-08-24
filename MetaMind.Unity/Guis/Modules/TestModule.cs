namespace MetaMind.Unity.Guis.Modules
{
    using System;
    using System.Speech.Synthesis;
    using Concepts.Tests;
    using Engine.Guis;
    using Engine.Screens;
    using Layers;
    using Screens;

    public class TestModule : Module<TestModuleSettings>
    {
        private readonly ITest test;

        private readonly TestSession testSession;

        private readonly IGameLayer testLayer;

        public TestModule(
            TestModuleSettings settings,
            ITest             test,
            TestSession       testSession,
            SpeechSynthesizer testSynthesizer,
            IGameLayer        testLayer)
            : this(settings, test, testSession, testSynthesizer)
        {
            if (testLayer == null)
            {
                throw new ArgumentNullException(nameof(testLayer));
            }

            this.testLayer = testLayer;
        }

        private TestModule(TestModuleSettings settings, ITest test, TestSession testSession, SpeechSynthesizer testSynthesizer)
            : base(settings)
        {
            if (test == null)
            {
                throw new ArgumentNullException(nameof(test));
            }

            if (testSession == null)
            {
                throw new ArgumentNullException(nameof(testSession));
            }

            if (testSynthesizer == null)
            {
                throw new ArgumentNullException(nameof(testSynthesizer));
            }

            this.test        = test;
            this.testSession = testSession;
            Test.Session = this.testSession;
            Test.Speech  = testSynthesizer;

            this.Logic  = new TestModuleLogic(this, this.test, this.testSession);
            this.Visual = new TestModuleVisual(this, this.test);
        }
    }
}
