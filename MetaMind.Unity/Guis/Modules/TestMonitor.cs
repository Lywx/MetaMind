namespace MetaMind.Unity.Guis.Modules
{
    using System;
    using Concepts.Tests;
    using Engine;
    using Engine.Components;
    using Engine.Service.Loader;
    using Microsoft.Xna.Framework;

    public class TestMonitor : MMInputableComponent, IConfigurationLoader
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

        public string ConfigurationFile => "Unity.txt";

        public void LoadConfiguration()
        {
            var pairs = ConfigurationLoader.LoadUniquePairs(this);

            TestWarningRate = FileLoader.ReadValueFloats(pairs, "TestMonitor.TestWarningRate", 0, 10f);
        }

        #endregion
    }
}
