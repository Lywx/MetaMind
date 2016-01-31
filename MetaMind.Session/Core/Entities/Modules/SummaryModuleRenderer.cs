namespace MetaMind.Session.Guis.Modules
{
    using Engine.Core.Entity;
    using Engine.Core.Entity.Common;
    using Extensions;
    using Microsoft.Xna.Framework;
    using Runtime;
    using Runtime.Attention;
    using Summary;

    public class SummaryModuleRenderer : MMMVCEntityRenderer<SummaryModule, SummarySettings, SummaryModuleController>
    {
        public SummaryModuleRenderer(SummaryModule module, IConsciousness consciousness, ISynchronizationData synchronizationData)
            : base(module)
        {
            this.Consciousness   = consciousness;
            this.SynchronizationData = synchronizationData;

            this.Entities = new MMEntityCollection<IMMVisualEntity>();
        }

        private IConsciousness Consciousness { get; set; }

        private MMEntityCollection<IMMVisualEntity> Entities { get; set; }

        private ISynchronizationData SynchronizationData { get; set; }

        public override void LoadContent()
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
                    () => this.SynchronizationData.SynchronizedHourYesterday.ToSummary(),
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
                    () => (this.SynchronizationData.SynchronizedHourYesterday - this.Settings.HourOfGood - this.Settings.HourOfLofty).ToSummary(),
                    () => (this.SynchronizationData.SynchronizedHourYesterday - this.Settings.HourOfGood - this.Settings.HourOfLofty >= 0 ? Color.White : Color.Red)));

            // Weekly statistics
            this.Entities.Add(
                factory.CreateEntry(
                    8,
                    "Hours in Synchronization In Recent 7 Days:",
                    () => ((int)this.SynchronizationData.SynchronizedTimeRecentWeek.TotalHours).ToSummary(),
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
                    () => ((int)this.SynchronizationData.SynchronizedTimeRecentWeek.TotalHours - this.Settings.HourOfWorldRecord).ToSummary(),
                    () => ((int)this.SynchronizationData.SynchronizedTimeRecentWeek.TotalHours - this.Settings.HourOfWorldRecord >= 0
                         ? Color.Gold
                         : Color.Red)));

            base.LoadContent();
        }

        public override void Draw(GameTime time)
        {
            this.Entities.Draw(time);
        }
    }
}