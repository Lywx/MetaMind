namespace MetaMind.Unity.Components
{
    using System;
    using Engine;
    using Engine.Components.Profiler;
    using Engine.Service.Loader;
    using Microsoft.Xna.Framework;

    public class ResourceMonitor : GameInputableComponent, IConfigurationLoader
    {
        public ResourceMonitor(GameEngine engine) : base(engine)
        {
            this.Processor = new ProcessorProfiler();

            this.LoadConfiguration();
        }

        private float WarningCpuUsagePercentage { get; set; }

        private DateTime WarningMoment { get; set; } = DateTime.Now;

        private TimeSpan WarningCpuUsageInterval { get; set; }

        private ProcessorProfiler Processor { get; set; }

        public override void Update(GameTime gameTime)
        {
            this.Processor.UpdateSample();

            var shouldWarningRepeat = DateTime.Now - this.WarningMoment > this.WarningCpuUsageInterval;
            var shouldWarn = this.Processor.CpuUsagePercentage > this.WarningCpuUsagePercentage;

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

            this.WarningCpuUsageInterval = TimeSpan.FromMinutes(FileLoader.ReadValueFloats(pairs, "ResourceMonitor.WarningCPUUsageInterval", 0, 5f));
            this.WarningCpuUsagePercentage = FileLoader.ReadValueFloats(pairs, "ResourceMonitor.WarningCPUUsagePercentage", 0, 10f);
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            this.Processor?.Dispose();

            base.Dispose(disposing);
        }
    }
}
