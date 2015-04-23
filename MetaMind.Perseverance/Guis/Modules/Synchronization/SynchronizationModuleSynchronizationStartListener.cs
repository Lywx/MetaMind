namespace MetaMind.Perseverance.Guis.Modules.Synchronization
{
    using MetaMind.Engine.Components.Events;
    using MetaMind.Perseverance.Concepts;
    using MetaMind.Perseverance.Events;
    using MetaMind.Perseverance.Sessions;

    internal class SynchronizationModuleSynchronizationStartListener : Listener
    {
        private readonly ISynchronization      synchronization;
        private readonly SynchronizationModule synchronizationModule;

        public SynchronizationModuleSynchronizationStartListener(ISynchronization synchronization, SynchronizationModule synchronizationModule)
        {
            this.synchronizationModule = synchronizationModule;
            this.synchronization = synchronization;

            this.RegisteredEvents.Add((int)SessionEventType.SyncStarted);
        }

        public override bool HandleEvent(IEvent e)
        {
            var synchronizationStartedEventArgs = (SynchronizationStartedEventArgs)e.EventData;
            var data = synchronizationStartedEventArgs.Task;

            // uncomment this to enforce fixed entry start/stop
            //// if (synchronization.IsEnabled) return true;

            this.synchronizationModule.StartSynchronizing(data);

            return true;
        }
    }
}