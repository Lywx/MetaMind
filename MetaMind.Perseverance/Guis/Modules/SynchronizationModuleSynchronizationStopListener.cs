namespace MetaMind.Perseverance.Guis.Modules
{
    using MetaMind.Engine.Components.Events;
    using MetaMind.Perseverance.Concepts;
    using MetaMind.Perseverance.Concepts.Cognitions;
    using MetaMind.Perseverance.Sessions;

    internal class SynchronizationModuleSynchronizationStopListener : ListenerBase
    {
        private readonly ISynchronization   synchronization;
        private readonly SynchronizationModule synchronizationModule;

        public SynchronizationModuleSynchronizationStopListener(ISynchronization synchronization, SynchronizationModule synchronizationModule)
        {
            this.synchronization    = synchronization;
            this.synchronizationModule = synchronizationModule;

            this.RegisteredEvents.Add((int)SessionEventType.SyncStopped);
        }

        public override bool HandleEvent(EventBase @event)
        {
            if (!this.synchronization.Enabled)
            {
                return true;
            }

            this.synchronizationModule.StopSynchronizing();

            return true;
        }
    }
}