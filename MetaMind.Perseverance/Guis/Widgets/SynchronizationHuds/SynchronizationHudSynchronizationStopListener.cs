using MetaMind.Engine.Components.Events;
using MetaMind.Perseverance.Concepts.Cognitions;
using MetaMind.Perseverance.Sessions;

namespace MetaMind.Perseverance.Guis.Widgets.SynchronizationHuds
{
    internal class SynchronizationHudSynchronizationStopListener : ListenerBase
    {
        private readonly Synchronization   synchronization;
        private readonly SynchronizationHud synchronizationHud;

        public SynchronizationHudSynchronizationStopListener(Synchronization synchronization, SynchronizationHud synchronizationHud)
        {
            this.synchronization    = synchronization;
            this.synchronizationHud = synchronizationHud;

            RegisteredEvents.Add( ( int ) AdventureEventType.SyncStopped );
        }

        public override bool HandleEvent( EventBase @event )
        {
            if ( !synchronization.Enabled )
                return true;

            synchronizationHud.StopSynchronizing();

            return true;
        }
    }
}