namespace MetaMind.Runtime.Guis.Modules.Synchronization
{
    using System;

    using MetaMind.Engine.Components.Events;
    using MetaMind.Runtime.Concepts.Synchronizations;
    using MetaMind.Runtime.Events;
    using MetaMind.Runtime.Sessions;

    internal class SynchronizationModuleSynchronizationStartListener : Listener
    {
        private readonly ISynchronization synchronization;

        public SynchronizationModuleSynchronizationStartListener(ISynchronization synchronization)
        {
            if (synchronization == null)
            {
                throw new ArgumentNullException("synchronization");
            }

            this.synchronization = synchronization;

            this.RegisteredEvents.Add((int)SessionEventType.SyncStarted);
        }

        public override bool HandleEvent(IEvent e)
        {
            var synchronizationStartedEventArgs = (SynchronizationStartedEventArgs)e.EventData;
            var data = synchronizationStartedEventArgs.Data;

            // uncomment this to enforce fixed entry start/stop
            //// if (synchronization.IsEnabled) return true;

            this.synchronization.TryStart(data);

            return true;
        }
    }
}