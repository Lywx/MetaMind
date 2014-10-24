using System;
using System.Collections.Generic;
using System.Diagnostics;
using MetaMind.Engine.Guis.Widgets.Cameras;
using MetaMind.Perseverance.Concepts.TaskEntries;
using MetaMind.Perseverance.Guis.Widgets.TileWidgets.Plannings;
using MetaMind.Perseverance.Settings;
using MetaMind.Engine.Components.Inputs;
using MetaMind.Engine.Guis.Widgets.Cameras;
using MetaMind.Engine.Guis.Widgets.Items;
using MetaMind.Engine.Settings;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis.Modules
{
    public class PlanningModuleControl : ModuleControl<PlanningModule, PlanningModuleSettings, PlanningModuleControl>
    {
        //---------------------------------------------------------------------
        private IMarkovCamera   camera;
        //---------------------------------------------------------------------
        private ChildLivingWindowListener childLivingListener;
        private DirectionWindow directionWindow;
        private FutureWindow    futureWindow;
        private QuestionWindow  questionWindow;
        //---------------------------------------------------------------------
        public List<LivingWindow> Windows { get; set; }
        private LivingWindow HighlightedWindow
        {
            get { return Windows.Find( window => window.IsCurrent ); }
        }

        //---------------------------------------------------------------------

        #region Contructors

        public PlanningModuleControl( PlanningModule module )
            : base( module )
        {
            questionWindow  = new QuestionWindow( "Questions", ModuleSettings.QuestionWindowX, ModuleSettings.QuestionWindowY, TileViewSettings.Default, ItemSettings.Default, isRoot: true );
            directionWindow = new DirectionWindow( "Directions", ModuleSettings.DirectionWindowX, ModuleSettings.DirectionWindowY, TileViewSettings.Default, ItemSettings.Default, isRoot: true );
            futureWindow    = new FutureWindow( "Futures", ModuleSettings.FutureWindowX, ModuleSettings.FutureWindowY, TileViewSettings.Default, ItemSettings.Default, isRoot: true );

            questionWindow .SingleDropped   += QuestionWindow_SingleDropped;
            questionWindow .MultipleDropped += QuestionWindow_MultipleDropped;
            directionWindow.SingleDropped   += DirectionWindow_SingleDropped;
            directionWindow.MultipleDropped += DirectionWindow_MultipleDropped;

            camera = new MarkovCamera( CameraSettings.Default );

            Windows = new List<LivingWindow>();
        }

        #endregion Contructors

        //---------------------------------------------------------------------

        #region Load and Unload

        public override void Load( IModuleData data )
        {
            LoadGui();
            LoadEvents();
            LoadData( ( PlanningModuleData ) data );
        }

        public override void Unload()
        {
            UnloadGui();
            UnloadEvents();
        }

        private void LoadData( PlanningModuleData data )
        {
            data.QuestionData .ForEach( question  => questionWindow .AddItem( question ) );
            data.DirectionData.ForEach( direction => directionWindow.AddItem( direction ) );
            data.FutureData   .ForEach( future    => futureWindow   .AddItem( future ) );
        }
        private void LoadEvents()
        {
            Debug.Assert( childLivingListener == null );

            childLivingListener = new ChildLivingWindowListener( this );
            EventManager.AddListener( childLivingListener );
        }

        private void LoadGui()
        {
            //questionWindow .Load(); TODO
            //directionWindow.Load();
            //futureWindow   .Load();
            
            Windows.AddRange( new List<LivingWindow> { questionWindow, directionWindow, futureWindow } );
        }

        private void UnloadEvents()
        {
            Debug.Assert( childLivingListener != null );

            EventManager.RemoveListener( childLivingListener );
            childLivingListener = null;
        }

        private void UnloadGui()
        {
            questionWindow .Close();
            directionWindow.Close();
            futureWindow   .Close();
        }

        #endregion Load and Unload

        #region Events

        private void DirectionWindow_MultipleDropped( object sender, EventArgs e )
        {
            if ( !futureWindow.IsReceiver )
                return;

            var mouseOverItem = futureWindow.Items.Find( item => item.IsMouseOver );
            if ( mouseOverItem == null )
                return;

            foreach ( var item in directionWindow.ItemsSelected )
            {
                var draggingDirectionItem = ( DirectionItem ) item;
                var draggingDirectionData = ( DirectionEntry ) draggingDirectionItem.LivingData;
                var mouseOverFutureItem = ( FutureItem ) mouseOverItem;
                var mouseOverFutureData = ( FutureEntry ) mouseOverFutureItem.LivingData;

                if ( draggingDirectionData.HasUpgrade )
                {
                    draggingDirectionData.Upgrade.Downgrades.Remove( draggingDirectionData );
                }
                draggingDirectionData.Upgrade = mouseOverFutureData;
                mouseOverFutureData.Downgrades.Add( draggingDirectionData );
            }
        }

        private void DirectionWindow_SingleDropped( object sender, EventArgs e )
        {
            if ( !futureWindow.IsReceiver )
                return;

            var mouseOverItem = futureWindow.Items.Find( item => item.IsMouseOver );
            if ( mouseOverItem == null )
                return;

            var draggingDirectionItem = ( DirectionItem ) directionWindow.ItemSelected;
            var draggingDirectionData = ( DirectionEntry ) draggingDirectionItem.LivingData;
            var mouseOverFutureItem = ( FutureItem ) mouseOverItem;
            var mouseOverFutureData = ( FutureEntry ) mouseOverFutureItem.LivingData;

            if ( draggingDirectionData.HasUpgrade )
            {
                draggingDirectionData.Upgrade.Downgrades.Remove( draggingDirectionData );
            }
            draggingDirectionData.Upgrade = mouseOverFutureData;
            mouseOverFutureData.Downgrades.Add( draggingDirectionData );
        }

        private void QuestionWindow_MultipleDropped( object sender, EventArgs e )
        {
            if ( !directionWindow.IsReceiver )
                return;

            var mouseOverItem = directionWindow.Items.Find( item => item.IsMouseOver );
            if ( mouseOverItem == null )
                return;

            foreach ( var item in questionWindow.ItemsSelected )
            {
                var draggingQuestionItem = ( QuestionItem ) item;
                var draggingQuestionData = ( QuestionEntry ) draggingQuestionItem.LivingData;
                var mouseOverDirectionItem = ( DirectionItem ) mouseOverItem;
                var mouseOverDirectionData = ( DirectionEntry ) mouseOverDirectionItem.LivingData;

                if ( draggingQuestionData.HasUpgrade )
                {
                    draggingQuestionData.Upgrade.Downgrades.Remove( draggingQuestionData );
                    draggingQuestionData.Upgrade = null;
                }
                draggingQuestionData.Upgrade = mouseOverDirectionData;
                mouseOverDirectionData.Downgrades.Add( draggingQuestionData );
            }
        }

        private void QuestionWindow_SingleDropped( object sender, EventArgs e )
        {
            if ( !directionWindow.IsReceiver )
                return;

            var mouseOverItem = ( LivingDataItem ) directionWindow.Items.Find( item => item.IsMouseOver );
            if ( mouseOverItem == null )
                return;

            var draggingQuestionItem = ( QuestionItem ) questionWindow.ItemSelected;
            var draggingQuestionData = ( QuestionEntry ) draggingQuestionItem.LivingData;
            var mouseOverDirectionItem = ( DirectionItem ) mouseOverItem;
            var mouseOverDirectionData = ( DirectionEntry ) mouseOverDirectionItem.LivingData;

            if ( draggingQuestionData.HasUpgrade )
            {
                draggingQuestionData.Upgrade.Downgrades.Remove( draggingQuestionData );
                draggingQuestionData.Upgrade = null;
            }
            draggingQuestionData.Upgrade = mouseOverDirectionData;
            mouseOverDirectionData.Downgrades.Add( draggingQuestionData );
        }

        #endregion Events

        #region Update

        public override void HandleInput()
        {
            camera                           .HandleInput();
            Windows.ForEach( window => window.HandleInput() );
        }

        public override void UpdateInput( GameTime gameTime )
        {
            var keyboard = InputSequenceManager.Keyboard;
            if ( HighlightedWindow != null &&
                HighlightedWindow.ItemSelected != null )
            {
                if ( keyboard.IsActionTriggered( Actions.PullItem ) )
                {
                    HighlightedWindow.ItemSelected.Pull();
                }
                if ( keyboard.IsActionTriggered( Actions.StretchItem ) )
                {
                    HighlightedWindow.ItemSelected.Stretch();
                }
            }
        }

        public override void UpdateStructure( GameTime gameTime )
        {
            UpdateWindows( gameTime );
            UpdateWindowsChaining();
            UpdateWindowsPositioning();
            UpdateWindowWideExchange();
            UpdateCameraPanning( gameTime );
        }

        private void UpdateCameraPanning( GameTime gameTime )
        {
            camera.Update( gameTime );
            Windows.ForEach( window => window.TileWindowX += ( int ) camera.Movement.X );
        }

        private void UpdateWindows( GameTime gameTime )
        {
            Windows.ForEach( window => window.Update( gameTime ) );
        }
        private void UpdateWindowsChaining()
        {
            Windows.RemoveAll( window => !window.IsActive && !window.IsRoot );
        }

        private void UpdateWindowsPositioning()
        {
            // position streching
            // excluding the leftmost view
            for ( var i = 1 ; i < Windows.Count ; ++i )
            {
                var deltaX = Windows[ i ].TileWindowX - ( Windows[ i - 1 ].TileWindowX + Windows[ i - 1 ].TileWindowWidth + ModuleSettings.WindowMargin.X );
                if ( deltaX < -1 )
                    Windows[ i ].TileWindowX += ( int ) ( Math.Abs( deltaX ) * ModuleSettings.WindowSlidingElasticCoefficient );
                else if ( 1 < deltaX )
                    Windows[ i ].TileWindowX -= ( int ) ( Math.Abs( deltaX ) * ModuleSettings.WindowSlidingElasticCoefficient );
                else
                    Windows[ i ].TileWindowX = Windows[ i - 1 ].TileWindowX + Windows[ i - 1 ].TileWindowWidth + ModuleSettings.WindowMargin.X;
            }
        }

        private void UpdateWindowWideExchange()
        {
            if ( questionWindow.IsItemMultipleDragging || questionWindow.IsItemSingleDragging )
                directionWindow.IsReceiver = true;
            else
                directionWindow.IsReceiver = false;
            if ( directionWindow.IsItemMultipleDragging || directionWindow.IsItemSingleDragging )
                futureWindow.IsReceiver = true;
            else
                futureWindow.IsReceiver = false;
        }

        #endregion Update
    }
}