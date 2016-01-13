namespace MetaMind.Session.Components
{
    using System;
    using Engine;
    using Engine.Components;
    using Engine.Components.Profiler;
    using Engine.Services.IO;
    using Microsoft.Xna.Framework;

    public class MMPerformanceMonitor : MMInputableComponent, IPlainConfigurationFileLoader
    {
        public MMPerformanceMonitor(MMEngine engine) : base(engine)
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

                SessionGame.Session.Controller.Speech.SpeakAsync($"CPU usage percentage reached {this.Processor.CpuUsagePercentage.ToString("F1")}");
            }

            base.Update(gameTime);
        }

        #region Configurations

        public string ConfigurationFilename => "Session.txt";

        public void LoadConfiguration()
        {
            var pairs = PlainConfigurationLoader.LoadUnique(this);

            this.WarningCpuUsageInterval = TimeSpan.FromMinutes(PlainConfigurationReader.ReadValueFloat(pairs, "ResourceMonitor.WarningCPUUsageInterval", 5f));
            this.WarningCpuUsagePercentage = PlainConfigurationReader.ReadValueFloat(pairs, "ResourceMonitor.WarningCPUUsagePercentage", 10f);
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            this.Processor?.Dispose();

            base.Dispose(disposing);
        }
    }
}
