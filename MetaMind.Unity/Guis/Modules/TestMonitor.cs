namespace MetaMind.Unity.Guis.Modules
{
    using System;
    using Concepts.Tests;
    using Engine;
    using Engine.Settings.Loaders;

    using Microsoft.Xna.Framework;

    public class TestMonitor : GameControllableComponent, IConfigurationLoader
    {
        public static float TestWarningRate = 10f;

        private readonly string testWarningCue = "Test Warning";

        private readonly ITest test;

        public TestMonitor(GameEngine engine, ITest test) : base(engine)
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

        public string ConfigurationFile => "Unity.txt";

        public void LoadConfiguration()
        {
            var pairs = ConfigurationLoader.LoadUniquePairs(this);

            TestWarningRate = FileLoader.ExtractFloats(pairs, "TestMonitor.TestWarningRate", 0, 10f);
        }

        #endregion
    }
}
