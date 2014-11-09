using MetaMind.Engine.Components.Inputs;
using MetaMind.Engine.Guis.Elements.Regions;
using MetaMind.Engine.Guis.Widgets.ViewItems;
using MetaMind.Engine.Guis.Widgets.Views;
using MetaMind.Perseverance.Concepts.TaskEntries;
using MetaMind.Perseverance.Guis.Widgets.Tasks.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MetaMind.Perseverance.Guis.Widgets.Tasks.Views
{
    public class TaskViewControl : ViewControl2D
    {
        #region Constructors

        public TaskViewControl( IView view, TaskViewSettings viewSettings, TaskItemSettings itemSettings )
            : base( view, viewSettings, itemSettings )
        {
            Region      = new TaskViewRegion( view, viewSettings, itemSettings );
            ScrollBar   = new TaskViewScrollBar( view, viewSettings, itemSettings, viewSettings.ScrollBarSettings );
            ItemFactory = new TaskItemFactory();
        }

        #endregion Constructors

        #region Public Properties

        public TaskItemFactory ItemFactory { get; private set; }

        public TaskViewRegion Region { get; private set; }

        public TaskViewScrollBar ScrollBar { get; private set; }

        #endregion Public Properties

        #region Update

        public override void UpdateInput( GameTime gameTime )
        {
            if ( View.IsEnabled( ViewState.View_Active ) && 
                 View.IsEnabled( ViewState.View_Has_Focus ) && 
                !View.IsEnabled( ViewState.Item_Editting ) )
            {
                //------------------------------------------------------------------
                // mouse
                if ( InputSequenceManager.Mouse.IsWheelScrolledUp )
                {
                    ScrollBar.Trigger();
                    Scroll.MoveUp();
                }
                if ( InputSequenceManager.Mouse.IsWheelScrolledDown )
                {
                    Scroll.MoveDown();
                    ScrollBar.Trigger();
                }
                //------------------------------------------------------------------
                // keyboard
                // up down left right esc
                if ( InputSequenceManager.Keyboard.IsActionTriggered( Actions.Left ) )
                {
                    MoveLeft();
                }
                if ( InputSequenceManager.Keyboard.IsActionTriggered( Actions.Right ) )
                {
                    MoveRight();
                }
                if ( InputSequenceManager.Keyboard.IsActionTriggered( Actions.Up ) )
                {
                    MoveUp();
                }
                if ( InputSequenceManager.Keyboard.IsActionTriggered( Actions.Down ) )
                {
                    MoveDown();
                }
                if ( InputSequenceManager.Keyboard.IsActionTriggered( Actions.SUp ) )
                {
                    for ( var i = 0 ; i < ViewSettings.RowNumDisplay; i++ )
                    {
                        MoveUp();
                    }
                }
                if ( InputSequenceManager.Keyboard.IsActionTriggered( Actions.SDown ) )
                {
                    for ( var i = 0 ; i < ViewSettings.RowNumDisplay; i++ )
                    {
                        MoveDown();
                    }
                }
                //-----------------------------------------------------------------
                if ( InputSequenceManager.Keyboard.IsActionTriggered( Actions.TaskCreateItem ) )
                    AddItem();
                if ( InputSequenceManager.Keyboard.IsActionTriggered( Actions.Escape ) )
                    Selection.Clear();
            }
        }

        private void MoveDown()
        {
            ScrollBar.Trigger();
            Selection.MoveDown();
        }

        private void MoveUp()
        {
            ScrollBar.Trigger();
            Selection.MoveUp();
        }

        private void MoveRight()
        {
            ScrollBar.Trigger();
            Selection.MoveRight();
        }

        private void MoveLeft()
        {
            ScrollBar.Trigger();
            Selection.MoveLeft();
        }

        public override void UpdateStrucutre( GameTime gameTime )
        {
            base     .UpdateStrucutre( gameTime );
            ScrollBar.Upadte( gameTime );
            Region   .Update( gameTime );
        }

        protected override void UpdateViewLogics()
        {
            base.UpdateViewLogics();

            if ( Region.IsEnabled( RegionState.Region_Hightlighted ) )
                View.Enable( ViewState.View_Has_Focus );
        }

        #endregion Update

        #region Operations

        public virtual void AddItem( TaskEntry entry )
        {
            View.Items.Add( new ViewItemExchangable( View, ViewSettings, ItemSettings, ItemFactory, entry ) );
        }

        public virtual void AddItem()
        {
            View.Items.Add( new ViewItemExchangable( View, ViewSettings, ItemSettings, ItemFactory ) );
        }

        #endregion Operations
    }
}