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
        private readonly IConsciousness consciousness;

        private readonly ISynchronization synchronization;

        public SynchronizationModule(ICognition cognition, SynchronizationSettings settings)
            : base(settings)
        {
            if (cognition == null)
            {
                throw new ArgumentNullException("cognition");
            }

            this.consciousness   = cognition.Consciousness;
            this.synchronization = cognition.Synchronization;

            this.Logic  = new SynchronizationModuleLogic(this, this.consciousness, this.synchronization);
            this.Visual = new SynchronizationModuleVisual(this, cognition, this.consciousness, this.synchronization);
        }
    }
}