using MetaMind.Engine.Components.Events;
using MetaMind.Perseverance.Components;
using MetaMind.Perseverance.Concepts.Cognitions;
using MetaMind.Perseverance.Extensions;
using MetaMind.Perseverance.Guis;
using MetaMind.Perseverance.Guis.Modules;
using MetaMind.Perseverance.Guis.Widgets.SynchronizationHuds;

namespace MetaMind.Perseverance.Sessions
{
    internal class AdventureSleepStartedEventListener : ListenerBase
    {
        //---------------------------------------------------------------------
        private readonly Synchronization   synchronization;
        private readonly SynchronizationHud synchronizationHud;

        private readonly IModule            planning;
        private readonly IModule            tactic;
        
        public AdventureSleepStartedEventListener( AdventureConcept concepts, ComputingGui guis )
        {
            //-----------------------------------------------------------------
            synchronization    = synchronization;
            synchronizationHud = guis.SynchronizationHud;

            //-----------------------------------------------------------------
            planning           = guis.PlanningModule;
            tactic             = guis.TacticModule;

            //-----------------------------------------------------------------
            RegisteredEvents.Add( ( int ) AdventureEventType.SleepStarted );
        }

        public override bool HandleEvent( EventBase @event )
        {
            //-----------------------------------------------------------------
            if ( synchronization.Enabled )
                synchronizationHud.StopSynchronizing();
            synchronization.ResetForTomorrow();

            //-----------------------------------------------------------------
            planning.Unload();
            tactic  .Unload();

            return true;
        }
    }
}