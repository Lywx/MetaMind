namespace MetaMind.Runtime.Guis.Modules.Synchronization
{
    using System;

    using MetaMind.Engine.Components.Events;
    using MetaMind.Runtime.Concepts.Synchronizations;
    using MetaMind.Runtime.Events;
    using MetaMind.Runtime.Sessions;

    internal class SynchronizationModuleSynchronizationStartListener : Listener
    {
        public SynchronizationModuleSynchronizationStartListener(ISynchronization synchronization)
        {
            if (synchronization == null)
            {
                throw new ArgumentNullException("synchronization");
            }

            this.Synchronization = synchronization;

            this.RegisteredEvents.Add((int)SessionEventType.SyncStarted);
        }

        private ISynchronization Synchronization { get; set; }

        public override bool HandleEvent(IEvent e)
        {
            var synchronizationStartedEventArgs = (SynchronizationStartedEventArgs)e.EventData;
            var data = synchronizationStartedEventArgs.Data;

            // uncomment this to enforce fixed entry start/stop
            //// if (Synchronization.IsEnabled) return true;

            this.Synchronization.TryStart(data);

            return true;
        }
    }
}