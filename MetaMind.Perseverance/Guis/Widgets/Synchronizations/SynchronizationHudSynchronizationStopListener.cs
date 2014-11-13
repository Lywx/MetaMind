using MetaMind.Engine.Components.Events;
using MetaMind.Perseverance.Sessions;

namespace MetaMind.Perseverance.Guis.Widgets.Synchronizations
{
    using MetaMind.Perseverance.Concepts.Cognitions;

    internal class SynchronizationHudSynchronizationStopListener : ListenerBase
    {
        private readonly ISynchronization   synchronization;
        private readonly SynchronizationHud synchronizationHud;

        public SynchronizationHudSynchronizationStopListener(ISynchronization synchronization, SynchronizationHud synchronizationHud)
        {
            this.synchronization = synchronization;
            this.synchronizationHud = synchronizationHud;

            RegisteredEvents.Add((int)AdventureEventType.SyncStopped);
        }

        public override bool HandleEvent(EventBase @event)
        {
            if (!synchronization.Enabled)
            {
                return true;
            }

            synchronizationHud.StopSynchronizing();

            return true;
        }
    }
}