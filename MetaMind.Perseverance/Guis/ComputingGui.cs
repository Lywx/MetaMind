using System.Diagnostics;
using MetaMind.Engine.Components;
using MetaMind.Engine.Guis.Widgets;
using MetaMind.Engine.Guis.Widgets.Items;
using MetaMind.Engine.Guis.Widgets.Views;
using MetaMind.Perseverance.Guis.Modules;
using MetaMind.Perseverance.Guis.Widgets.Synchronizations;
using MetaMind.Perseverance.Sessions;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis
{
    public class ComputingGui : InputObject
    {
        //---------------------------------------------------------------------
        private AdventureSleepStartedEventListener sleepStartedEventListener;
        private AdventureSleepStoppedEventListener sleepStoppedEventListener;

        //---------------------------------------------------------------------
        private SynchronizationHud synchronizationHud;

        //---------------------------------------------------------------------

        #region Load and Unload

        /// <summary>
        /// Loads the specified concept.
        /// </summary>
        /// <remarks>
        /// Must be called only once during application.
        /// </remarks>
        public void Load()
        {
            LoadGuis();
            LoadEvents();
        }

        private void LoadEvents()
        {
            Debug.Assert( sleepStartedEventListener == null );
            Debug.Assert( sleepStoppedEventListener == null );

            sleepStartedEventListener = new AdventureSleepStartedEventListener( Concepts, this );
            EventManager.AddListener( sleepStartedEventListener );

            sleepStoppedEventListener = new AdventureSleepStoppedEventListener( Concepts, this );
            EventManager.AddListener( sleepStoppedEventListener );
        }

        private void LoadGuis()
        {
            // should not create a new instance when sleep stop
            // which casue duplicaate event listeners are created
            Debug.Assert( synchronizationHud == null );
            Debug.Assert( planning           == null );
            Debug.Assert( tactic             == null );

            synchronizationHud = new SynchronizationHud( Concepts.Cognition.Synchronization, SynchronizationHudSettings.Default );

            planning = new PlanningModule( PlanningModuleSettings.Default );
            tactic = new TacticModule( TacticModuleSettings.Default );

            if ( Concepts.Cognition.Awake )
            {
                planning.Load( new PlanningModuleData( Concepts.Tasklist ) );
                tactic  .Load( new TacticModuleData( Concepts.Tasklist ) );
            }
        }

        public void Unload()
        {
            UnloadGuis();
            UnloadEvents();
        }

        /// <summary>
        /// Unloads all guis.
        /// </summary>
        /// <remarks>
        /// Currently this function is incomplete.
        /// </remarks>
        private void UnloadGuis()
        {
            planning.Unload();
            tactic  .Unload();
            summary .Unload();
        }

        private void UnloadEvents()
        {
            Debug.Assert( sleepStartedEventListener != null );
            Debug.Assert( sleepStoppedEventListener != null );

            EventManager.RemoveListener( sleepStartedEventListener );
            EventManager.RemoveListener( sleepStoppedEventListener );
        }

        #endregion Load and Unload

        //---------------------------------------------------------------------

        #region Update and Draw

        public sealed override void Draw( GameTime gameTime )
        {
            //synchronizationHud.Draw( gameTime );

            //planning          .Draw( gameTime );
            //tactic            .Draw( gameTime );

            view.Draw( gameTime );
        }

        public sealed override void HandleInput()
        {
            base.HandleInput();

            //synchronizationHud.HandleInput();

            //planning          .HandleInput();
            //tactic            .HandleInput();

            view.HandleInput();
        }

        public sealed override void UpdateInput( GameTime gameTime )
        {
            view.UpdateInput( gameTime );
        }

        public sealed override void UpdateStructure( GameTime gameTime )
        {
            //synchronizationHud.Update( gameTime );

            //planning          .Update( gameTime );
            //tactic            .Update( gameTime );

            view.UpdateStructure( gameTime );
        }

        #endregion Update and Draw

        //---------------------------------------------------------------------
    }
}