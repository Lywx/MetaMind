namespace MetaMind.Session.Components
{
    using System;
    using Engine;
    using Engine.Core;
    using Engine.Core.Backend.Profiler;
    using Engine.Core.Services.IO;
    using Microsoft.Xna.Framework;

    public class MMPerformanceMonitor : ImmInputableComponent, IMMPlainConfigurationFileLoader
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

                MMSessionGame.Session.Controller.Speech.SpeakAsync($"CPU usage percentage reached {this.Processor.CpuUsagePercentage.ToString("F1")}");
            }

            base.Update(gameTime);
        }

        #region Configurations

        public string ConfigurationFilename => "Session.txt";

        public void LoadConfiguration()
        {
            var pairs = MMPlainConfigurationLoader.LoadUnique(this);

            this.WarningCpuUsageInterval = TimeSpan.FromMinutes(MMPlainConfigurationReader.ReadValueFloat(pairs, "ResourceMonitor.WarningCPUUsageInterval", 5f));
            this.WarningCpuUsagePercentage = MMPlainConfigurationReader.ReadValueFloat(pairs, "ResourceMonitor.WarningCPUUsagePercentage", 10f);
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            this.Processor?.Dispose();

            base.Dispose(disposing);
        }
    }
}
