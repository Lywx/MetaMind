namespace MetaMind.Session.Guis.Modules
{
    using System;
    using Engine.Entities;
    using Runtime;
    using Runtime.Attention;

    /// <summary>
    /// This module control all the interaction with the Synchronization and Consciousness object, 
    /// since Synchronization is only a data class.
    /// </summary>
    public class SynchronizationModule : MMMVCEntity<SynchronizationSettings>
    {
        private readonly IConsciousness consciousness;

        private readonly ISynchronizationData synchronizationData;

        public SynchronizationModule(ICognition cognition, SynchronizationSettings settings)
            : base(settings)
        {
            if (cognition == null)
            {
                throw new ArgumentNullException(nameof(cognition));
            }

            this.consciousness   = cognition.Consciousness;
            this.synchronizationData = cognition.SynchronizationData;

            this.Controller  = new SynchronizationController(this, this.consciousness, this.synchronizationData);
            this.Renderer = new SynchronizationRenderer(this, cognition, this.consciousness, this.synchronizationData);
        }
    }
}