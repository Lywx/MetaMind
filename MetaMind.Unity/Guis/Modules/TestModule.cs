﻿namespace MetaMind.Unity.Guis.Modules
{
    using System;
    using System.Speech.Synthesis;
    using Concepts.Tests;
    using Engine;
    using Engine.Gui.Modules;

    public class TestModule : GameMvcEntity<TesTMvcSettings>
    {
        private readonly ITest test;

        private readonly TestSession testSession;

        public TestModule(TesTMvcSettings settings, ITest test, TestSession testSession, SpeechSynthesizer testSynthesizer)
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

            this.Logic  = new TesTMvcLogic(this, this.test, this.testSession);
            this.Visual = new TesTMvcVisual(this, this.test);
        }
    }
}
