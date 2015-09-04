namespace MetaMind.Unity.Components
{
    using System;
    using Engine;
    using Engine.Components.Profilings;
    using Engine.Settings.Loaders;
    using Microsoft.Xna.Framework;

    public class ResourceMonitor : GameControllableComponent, IConfigurationLoader
    {
        public ResourceMonitor(GameEngine engine) : base(engine)
        {
            this.Processor = new ProcessorProfiler();

            this.LoadConfiguration();
        }

        private float WarningCPUUsagePercentage { get; set; }

        private DateTime WarningMoment { get; set; } = DateTime.Now;

        private TimeSpan WarningCPUUsageInterval { get; set; }

        private ProcessorProfiler Processor { get; set; }

        public override void Update(GameTime gameTime)
        {
            this.Processor.UpdateSample();

            var shouldWarningRepeat = DateTime.Now - this.WarningMoment > this.WarningCPUUsageInterval;
            var shouldWarn = this.Processor.CpuUsagePercentage > this.WarningCPUUsagePercentage;

            if (shouldWarningRepeat && shouldWarn)
            {
                this.WarningMoment = DateTime.Now;

                Unity.Speech.SpeakAsync($"CPU usage percentage reached {this.Processor.CpuUsagePercentage.ToString("F1")}");
            }

            base.Update(gameTime);
        }

        #region Configurations

        public string ConfigurationFile => "Unity.txt";

        public void LoadConfiguration()
        {
            var pairs = ConfigurationLoader.LoadUniquePairs(this);

            this.WarningCPUUsageInterval = TimeSpan.FromMinutes(FileLoader.ExtractFloats(pairs, "ResourceMonitor.WarningCPUUsageInterval", 0, 5f));
            this.WarningCPUUsagePercentage = FileLoader.ExtractFloats(pairs, "ResourceMonitor.WarningCPUUsagePercentage", 0, 10f);
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            this.Processor?.Dispose();

            base.Dispose(disposing);
        }
    }
}
