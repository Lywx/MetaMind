namespace MetaMind.Session.Guis.Modules
{
    using System;
    using Engine;
    using Engine.Core;
    using Engine.Core.Services.IO;
    using Microsoft.Xna.Framework;
    using Tests;

    public class TestMonitor : ImmInputableComponent, IMMPlainConfigurationFileLoader
    {
        public static float TestWarningRate = 10f;

        private readonly string testWarningCue = "Test Warning";

        private readonly ITest test;

        public TestMonitor(MMEngine engine, ITest test) : base(engine)
        {
            if (test == null)
            {
                throw new ArgumentNullException(nameof(test));
            }

            this.test = test;

            engine.Components.Add(this);

            this.LoadConfiguration();
        }

        public override void Update(GameTime gameTime)
        {
            if (this.test.Evaluation.ResultAllPassedRate < TestWarningRate)
            {
                this.GlobalInterop.Audio.PlayMusic(this.testWarningCue);
            }

            base.Update(gameTime);
        }

        #region Configurations

        public string ConfigurationFilename => "Session.txt";

        public void LoadConfiguration()
        {
            var pairs = MMPlainConfigurationLoader.LoadUnique(this);

            TestWarningRate = MMPlainConfigurationReader.ReadValueFloat(pairs, "TestMonitor.TestWarningRate", 10f);
        }

        #endregion
    }
}
