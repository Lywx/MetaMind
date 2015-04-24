namespace MetaMind.Runtime.Guis.Modules.Synchronization
{
    using MetaMind.Engine.Components.Events;
    using MetaMind.Runtime.Concepts.Synchronizations;
    using MetaMind.Runtime.Sessions;

    internal class SynchronizationModuleSynchronizationStopListener : Listener
    {
        private readonly ISynchronization synchronization;
        private readonly SynchronizationModule synchronizationModule;

        public SynchronizationModuleSynchronizationStopListener(ISynchronization synchronization, SynchronizationModule synchronizationModule)
        {
            this.synchronization = synchronization;
            this.synchronizationModule = synchronizationModule;

            this.RegisteredEvents.Add((int)SessionEventType.SyncStopped);
        }

        public override bool HandleEvent(IEvent @event)
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