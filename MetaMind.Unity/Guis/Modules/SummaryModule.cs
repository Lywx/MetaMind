namespace MetaMind.Unity.Guis.Modules
{
    using Concepts.Cognitions;
    using Concepts.Synchronizations;
    using Engine;
    using Engine.Gui.Modules;

    public class SummaryModule : GameEntityModule<SummarySettings>
    {
        public SummaryModule(IConsciousness consciousness, ISynchronization synchronization, SummarySettings settings)
            : base(settings)
        {
            this.Consciousness   = consciousness;
            this.Synchronization = synchronization;

            this.Logic  = new SummaryModuleLogic(this, this.Consciousness, this.Synchronization);
            this.Visual = new SummaryModuleVisual(this, this.Consciousness, this.Synchronization);
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