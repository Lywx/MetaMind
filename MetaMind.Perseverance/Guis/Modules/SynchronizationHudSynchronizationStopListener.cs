namespace MetaMind.Perseverance.Guis.Modules
{
    using MetaMind.Engine.Components.Events;
    using MetaMind.Perseverance.Concepts.Cognitions;
    using MetaMind.Perseverance.Sessions;

    internal class SynchronizationHudSynchronizationStopListener : ListenerBase
    {
        private readonly ISynchronization   synchronization;
        private readonly SynchronizationModule synchronizationModule;

        public SynchronizationHudSynchronizationStopListener(ISynchronization synchronization, SynchronizationModule synchronizationModule)
        {
            this.synchronization    = synchronization;
            this.synchronizationModule = synchronizationModule;

            this.RegisteredEvents.Add((int)AdventureEventType.SyncStopped);
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