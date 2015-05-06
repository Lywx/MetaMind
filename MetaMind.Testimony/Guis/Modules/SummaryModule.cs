namespace MetaMind.Testimony.Guis.Modules
{
    using MetaMind.Engine.Guis;
    using MetaMind.Testimony.Concepts.Cognitions;
    using MetaMind.Testimony.Concepts.Synchronizations;

    public class SummaryModule : Module<SummarySettings>
    {
        public SummaryModule(IConsciousness consciousness, ISynchronization synchronization, SummarySettings settings)
            : base(settings)
        {
            this.Consciousness   = consciousness;
            this.Synchronization = synchronization;

            this.Logic  = new SummaryModuleLogicControl(this, this.Consciousness, this.Synchronization);
            this.Visual = new SummaryModuleVisualControl(this, this.Consciousness, this.Synchronization);
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