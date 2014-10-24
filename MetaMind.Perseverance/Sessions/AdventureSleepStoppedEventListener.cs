using MetaMind.Engine.Components.Events;
using MetaMind.Perseverance.Components;
using MetaMind.Perseverance.Concepts.TaskEntries;
using MetaMind.Perseverance.Extensions;
using MetaMind.Perseverance.Guis;
using MetaMind.Perseverance.Guis.Modules;

namespace MetaMind.Perseverance.Sessions
{
    internal class AdventureSleepStoppedEventListener : ListenerBase
    {
        //---------------------------------------------------------------------
        private readonly Tasklist data;
        
        private readonly IModule        planning;
        private readonly IModule        tactic;

        public AdventureSleepStoppedEventListener( AdventureConcept concepts, ComputingGui guis )
        {
            //-----------------------------------------------------------------
            data = concepts.Tasklist;

            //-----------------------------------------------------------------
            planning = guis.PlanningModule;
            tactic   = guis.TacticModule;

            //-----------------------------------------------------------------
            RegisteredEvents.Add( ( int ) AdventureEventType.SleepStopped );
        }

        public override bool HandleEvent( EventBase @event )
        {
            planning.Reload( new PlanningModuleData( data ) );
            tactic  .Reload( new TacticModuleData( data ) );
            
            return true;
        }
    }
}