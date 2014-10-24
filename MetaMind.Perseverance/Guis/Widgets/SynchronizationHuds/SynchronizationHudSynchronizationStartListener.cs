using MetaMind.Engine.Components.Events;
using MetaMind.Perseverance.Concepts.Cognitions;
using MetaMind.Perseverance.Sessions;

namespace MetaMind.Perseverance.Guis.Widgets.SynchronizationHuds
{
    internal class SynchronizationHudSynchronizationStartListener : ListenerBase
    {
        private Synchronization synchronization;
        private readonly SynchronizationHud hud;

        public SynchronizationHudSynchronizationStartListener( Synchronization synchronization, SynchronizationHud hud )
        {
            this.hud = hud;
            this.synchronization = synchronization;

            RegisteredEvents.Add( ( int ) AdventureEventType.SyncStarted );
        }

        public override bool HandleEvent( EventBase @event )
        {
            var synchronizationStartedEventArgs = ( SynchronizationStartedEventArgs ) @event.Data;
            var data = synchronizationStartedEventArgs.TaskEntry;

            if ( synchronization.Enabled )
                return true;

            hud.StartSynchronizing( data );

            return true;
        }
    }
}