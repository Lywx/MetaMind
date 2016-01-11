namespace MetaMind.Session.Guis.Modules
{
    using Engine.Entities;
    using Runtime;
    using Runtime.Attention;

    public class SummaryModule : MMMVCEntity<SummarySettings>
    {
        public SummaryModule(IConsciousness consciousness, ISynchronizationData synchronizationData, SummarySettings settings)
            : base(settings)
        {
            this.Consciousness   = consciousness;
            this.SynchronizationData = synchronizationData;

            this.Controller  = new SummaryModuleController(this, this.Consciousness, this.SynchronizationData);
            this.Renderer = new SummaryModuleRenderer(this, this.Consciousness, this.SynchronizationData);
        }

        ~SummaryModule()
        {
            this.Dispose();
        }

        #region Dependency

        private IConsciousness Consciousness { get; set; }

        private ISynchronizationData SynchronizationData { get; set; }

        #endregion
    }
}