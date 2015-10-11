namespace MetaMind.Session.Guis.Modules
{
    using System;
    using Concepts.Cognitions;
    using Concepts.Synchronizations;
    using Engine.Entities;

    /// <summary>
    /// This module control all the interaction with the Synchronization and Consciousness object, 
    /// since Synchronization is only a data class.
    /// </summary>
    public class SynchronizationModule : MMMVCEntity<SynchronizationSettings>
    {
        private readonly IConsciousness consciousness;

        private readonly ISynchronization synchronization;

        public SynchronizationModule(ICognition cognition, SynchronizationSettings settings)
            : base(settings)
        {
            if (cognition == null)
            {
                throw new ArgumentNullException(nameof(cognition));
            }

            this.consciousness   = cognition.Consciousness;
            this.synchronization = cognition.Synchronization;

            this.Controller  = new SynchronizationController(this, this.consciousness, this.synchronization);
            this.Renderer = new SynchronizationRenderer(this, cognition, this.consciousness, this.synchronization);
        }
    }
}