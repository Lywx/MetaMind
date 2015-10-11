namespace MetaMind.Session.Guis.Modules
{
    using System;
    using System.Speech.Synthesis;
    using Concepts.Tests;
    using Engine.Entities;

    public class TestModule : MMMVCEntity<TesTMVCSettings>
    {
        private readonly ITest test;

        private readonly TestSession testSession;

        public TestModule(TesTMVCSettings settings, ITest test, TestSession testSession, SpeechSynthesizer testSynthesizer)
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

            this.Controller  = new TesTMVCController(this, this.test, this.testSession);
            this.Renderer = new TesTMVCRenderer(this, this.test);
        }
    }
}
