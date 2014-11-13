using MetaMind.Engine.Components.Events;
using MetaMind.Perseverance.Concepts.Cognitions;
using MetaMind.Perseverance.Sessions;

namespace MetaMind.Perseverance.Guis.Widgets.Synchronizations
{
    internal class SynchronizationHudSynchronizationStartListener : ListenerBase
    {
        private readonly ISynchronization   synchronization;
        private readonly SynchronizationHud synchronizationHud;

        public SynchronizationHudSynchronizationStartListener(ISynchronization synchronization, SynchronizationHud synchronizationHud)
        {
            this.synchronizationHud = synchronizationHud;
            this.synchronization = synchronization;

            RegisteredEvents.Add((int)AdventureEventType.SyncStarted);
        }

        public override bool HandleEvent(EventBase @event)
        {
            var synchronizationStartedEventArgs = (SynchronizationStartedEventArgs)@event.Data;
            var data = synchronizationStartedEventArgs.TaskEntry;

            // uncomment this to enforce fixed entry start/stop
            // if (synchronization.Enabled) return true;

            synchronizationHud.StartSynchronizing(data);

            return true;
        }
    }
}