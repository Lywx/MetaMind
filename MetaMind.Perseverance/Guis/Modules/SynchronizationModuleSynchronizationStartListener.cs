namespace MetaMind.Perseverance.Guis.Modules
{
    using MetaMind.Engine.Components.Events;
    using MetaMind.Perseverance.Concepts;
    using MetaMind.Perseverance.Concepts.Cognitions;
    using MetaMind.Perseverance.Events;
    using MetaMind.Perseverance.Sessions;

    internal class SynchronizationModuleSynchronizationStartListener : ListenerBase
    {
        private readonly ISynchronization   synchronization;
        private readonly SynchronizationModule synchronizationModule;

        public SynchronizationModuleSynchronizationStartListener(ISynchronization synchronization, SynchronizationModule synchronizationModule)
        {
            this.synchronizationModule = synchronizationModule;
            this.synchronization = synchronization;

            this.RegisteredEvents.Add((int)SessionEventType.SyncStarted);
        }

        public override bool HandleEvent(EventBase @event)
        {
            var synchronizationStartedEventArgs = (SynchronizationStartedEventArgs)@event.Data;
            var data = synchronizationStartedEventArgs.Task;

            // uncomment this to enforce fixed entry start/stop
            //// if (synchronization.IsEnabled) return true;

            this.synchronizationModule.StartSynchronizing(data);

            return true;
        }
    }
}