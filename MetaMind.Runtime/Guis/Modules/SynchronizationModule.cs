namespace MetaMind.Runtime.Guis.Modules
{
    using System;

    using MetaMind.Engine.Components.Inputs;
    using MetaMind.Engine.Guis;
    using MetaMind.Engine.Services;
    using MetaMind.Runtime.Concepts.Cognitions;
    using MetaMind.Runtime.Concepts.Synchronizations;
    using MetaMind.Runtime.Guis.Modules.Synchronization;

    using Microsoft.Xna.Framework;

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

            this.Control  = new SynchronizationModuleControl(this, this.Consciousness, this.Synchronization);
            this.Graphics = new SynchronizationModuleGraphics(this, this.Consciousness, this.Synchronization);
        }

        #endregion Constructors

        #region Dependency

        private IConsciousness Consciousness { get; set; }

        private ISynchronization Synchronization { get; set; }

        #endregion
    }
}