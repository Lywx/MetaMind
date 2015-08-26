namespace MetaMind.Unity.Components
{
    using System;
    using Engine;
    using Engine.Components.Profilings;
    using Engine.Settings.Loaders;
    using Microsoft.Xna.Framework;
    using GameComponent = Engine.GameComponent;

    public class ResourceMonitor : GameComponent, IConfigurationLoader
    {
        public ResourceMonitor(GameEngine engine) : base(engine)
        {
            this.Profiler = new ProcessorProfiler();
        }

        private float WarningCPUUsagePercentage { get; set; }

        private DateTime WarningMoment { get; set; } = DateTime.Now;

        private TimeSpan WarningInterval { get; set; } = TimeSpan.FromSeconds(5);

        private ProcessorProfiler Profiler { get; set; }

        public override void Update(GameTime gameTime)
        {
            var shouldWarningRepeat = DateTime.Now - this.WarningMoment > this.WarningInterval;
            if (shouldWarningRepeat && this.Profiler.CPUUsagePercentage() > this.WarningCPUUsagePercentage)
            {
                this.WarningMoment = DateTime.Now;

                Unity.Speech.SpeakAsync($"CPU usage percentage reached {this.Profiler.CPUUsagePercentage().ToString("F1")}");
            }

            base.Update(gameTime);
        }

        #region Configurations

        public string ConfigurationFile => "Unity.txt";

        public void LoadConfiguration()
        {
            var pairs = ConfigurationLoader.LoadUniquePairs(this);

            this.WarningCPUUsagePercentage = FileLoader.ExtractFloats(pairs, "ResourceMonitor.WarningCPUUsagePercentage", 0, 10);
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            this.Profiler?.Dispose();

            base.Dispose(disposing);
        }
    }
}
