namespace MetaMind.Session.Guis.Modules
{
    using Concepts.Cognitions;
    using Concepts.Synchronizations;
    using Engine.Entities;

    public class SummaryModule : MMMVCEntity<SummarySettings>
    {
        public SummaryModule(IConsciousness consciousness, ISynchronization synchronization, SummarySettings settings)
            : base(settings)
        {
            this.Consciousness   = consciousness;
            this.Synchronization = synchronization;

            this.Controller  = new SummaryModuleController(this, this.Consciousness, this.Synchronization);
            this.Renderer = new SummaryModuleRenderer(this, this.Consciousness, this.Synchronization);
        }

        ~SummaryModule()
        {
            this.Dispose();
        }

        #region Dependency

        private IConsciousness Consciousness { get; set; }

        private ISynchronization Synchronization { get; set; }

        #endregion
    }
}