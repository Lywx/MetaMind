﻿namespace MetaMind.Runtime.Guis.Modules
{
    using MetaMind.Engine;
    using MetaMind.Engine.Guis;
    using MetaMind.Engine.Services;
    using MetaMind.Runtime.Concepts.Cognitions;
    using MetaMind.Runtime.Concepts.Synchronizations;
    using MetaMind.Runtime.Extensions;
    using MetaMind.Runtime.Guis.Modules.Summary;

    using Microsoft.Xna.Framework;

    public class SummaryModuleGraphics : ModuleGraphics<SummaryModule, SummarySettings, SummaryModuleControl>
    {
        public SummaryModuleGraphics(SummaryModule module, IConsciousness consciousness, ISynchronization synchronization)
            : base(module)
        {
            this.Consciousness   = consciousness;
            this.Synchronization = synchronization;

            this.Entities = new GameVisualEntityCollection<IGameVisualEntity>();
        }

        private IConsciousness Consciousness { get; set; }

        private GameVisualEntityCollection<IGameVisualEntity> Entities { get; set; }

        private ISynchronization Synchronization { get; set; }

        public override void LoadContent(IGameInteropService interop)
        {
            var factory = new SummaryFactory(this.Settings);

            this.Entities.Add(
                new SummaryTitle(
                    () => this.Settings.TitleFont,
                    () => "Summary",
                    () => this.Settings.TitleCenter,
                    () => this.Settings.TitleColor,
                    () => this.Settings.TitleSize));

            // Daily statistics
            this.Entities.Add(
                factory.CreateEntry(
                    1,
                    "Hours in Synchronization:",
                    () => this.Synchronization.SynchronizedHourYesterday.ToSummary(),
                    Color.White));

            this.Entities.Add(
                factory.CreateEntry(
                    3,
                    "Hours in Good Profession:",
                    () => (-this.Settings.HourOfGood).ToSummary(),
                    Color.Red));
            this.Entities.Add(
                factory.CreateEntry(
                    4,
                    "Hours in Lofty Profession:",
                    () => (-this.Settings.HourOfLofty).ToSummary(),
                    Color.Red));

            this.Entities.Add(factory.CreateSplit(5, Color.Red));

            this.Entities.Add(
                factory.CreateEntry(
                    () => 6,
                    () => "",
                    () => (this.Synchronization.SynchronizedHourYesterday - this.Settings.HourOfGood - this.Settings.HourOfLofty).ToSummary(),
                    () => (this.Synchronization.SynchronizedHourYesterday - this.Settings.HourOfGood - this.Settings.HourOfLofty >= 0 ? Color.White : Color.Red)));

            // Weekly statistics
            this.Entities.Add(
                factory.CreateEntry(
                    8,
                    "Hours in Synchronization In Recent 7 Days:",
                    () => ((int)this.Synchronization.SynchronizedTimeRecentWeek.TotalHours).ToSummary(),
                    Color.White));

            this.Entities.Add(
                factory.CreateEntry(
                    10,
                    "Len Bosack's Records in 7 Days:",
                    () => (-this.Settings.HourOfWorldRecord).ToSummary(),
                    Color.Red));

            this.Entities.Add(factory.CreateSplit(11, Color.Red));

            this.Entities.Add(
                factory.CreateEntry(
                    () => 12,
                    () => "",
                    () => ((int)this.Synchronization.SynchronizedTimeRecentWeek.TotalHours - this.Settings.HourOfWorldRecord).ToSummary(),
                    () => ((int)this.Synchronization.SynchronizedTimeRecentWeek.TotalHours - this.Settings.HourOfWorldRecord >= 0
                         ? Color.Gold
                         : Color.Red)));

            base.LoadContent(interop);
        }

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            this.Entities.Draw(graphics, time, alpha);
        }
    }
}
