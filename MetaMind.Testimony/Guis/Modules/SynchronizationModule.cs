namespace MetaMind.Testimony.Guis.Modules
{
    using System;

    using MetaMind.Engine.Guis;
    using MetaMind.Testimony.Concepts.Cognitions;
    using MetaMind.Testimony.Concepts.Synchronizations;

    /// <summary>
    /// This module control all the interaction with the Synchronization and Consciousness object, 
    /// since Synchronization is only a data class.
    /// </summary>
    public class SynchronizationModule : Module<SynchronizationSettings>
    {
        #region Constructors

        public SynchronizationModule(IConsciousness consciousness, ISynchronization synchronization, SynchronizationSettings settings)
            : base(settings)
        {
            if (consciousness == null)
            {
                throw new ArgumentNullException("consciousness");
            }

            if (synchronization == null)
            {
                throw new ArgumentNullException("synchronization");
            }

            this.Consciousness   = consciousness;
            this.Synchronization = synchronization;

            this.Logic  = new SynchronizationModuleLogicControl(this, this.Consciousness, this.Synchronization);
            this.Visual = new SynchronizationModuleVisualControl(this, this.Consciousness, this.Synchronization);
        }

        #endregion Constructors

        #region Dependency

        private IConsciousness Consciousness { get; set; }

        private ISynchronization Synchronization { get; set; }

        #endregion
    }
}