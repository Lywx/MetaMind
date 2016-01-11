namespace MetaMind.Session.Guis.Modules
{
    using System;
    using Engine;
    using Engine.Components;
    using Engine.Services.Loader;
    using Microsoft.Xna.Framework;
    using Tests;

    public class TestMonitor : MMInputableComponent, IConfigurable
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
                this.Interop.Audio.PlayMusic(this.testWarningCue);
            }

            base.Update(gameTime);
        }

        #region Configurations

        public string ConfigurationFilename => "Session.txt";

        public void LoadConfiguration()
        {
            var pairs = ConfigurationLoader.LoadUniquePairs(this);

            TestWarningRate = InformationLoader.ReadValueFloats(pairs, "TestMonitor.TestWarningRate", 0, 10f);
        }

        #endregion
    }
}
