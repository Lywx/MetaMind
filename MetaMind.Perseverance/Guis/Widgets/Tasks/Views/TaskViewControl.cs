using MetaMind.Engine.Components.Inputs;
using MetaMind.Engine.Guis.Elements.Regions;
using MetaMind.Engine.Guis.Widgets.ViewItems;
using MetaMind.Engine.Guis.Widgets.Views;
using MetaMind.Perseverance.Guis.Widgets.Tasks.Items;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis.Widgets.Tasks.Views
{
    public class TaskViewControl : ViewControl2D
    {
        private TaskViewRegion    region;
        private TaskViewScrollBar scrollBar;
        private TaskItemFactory   itemFactory;

        #region Constructors

        public TaskViewControl( IView view, TaskViewSettings viewSettings, TaskItemSettings itemSettings )
            : base( view, viewSettings, itemSettings )
        {
            itemFactory = new TaskItemFactory();

            region = new TaskViewRegion( view, viewSettings, itemSettings );
            scrollBar = new TaskViewScrollBar( view, viewSettings, itemSettings, viewSettings.ScrollBarSettings );
        }

        #endregion Constructors

        #region Public Properties

        public TaskViewRegion Region
        {
            get { return region; }
        }

        public TaskViewScrollBar ScrollBar
        {
            get { return scrollBar; }
        }

        #endregion Public Properties

        #region Update

        public override void UpdateInput( GameTime gameTime )
        {
            if ( View.IsEnabled( ViewState.View_Active ) && View.IsEnabled( ViewState.View_Has_Focus ) &&
                !View.IsEnabled( ViewState.Item_Editting ) )
            {
                //------------------------------------------------------------------
                // mouse
                if ( InputSequenceManager.Mouse.IsWheelScrolledUp )
                    Scroll.MoveUp();
                if ( InputSequenceManager.Mouse.IsWheelScrolledDown )
                    Scroll.MoveDown();

                //------------------------------------------------------------------
                // keyboard
                // up down left right esc
                if ( InputSequenceManager.Keyboard.IsActionTriggered( Actions.Left ) )
                {
                    ScrollBar.Trigger();
                    Selection.MoveLeft();
                }
                if ( InputSequenceManager.Keyboard.IsActionTriggered( Actions.Right ) )
                {
                    ScrollBar.Trigger();
                    Selection.MoveRight();
                }
                if ( InputSequenceManager.Keyboard.IsActionTriggered( Actions.Up ) )
                {
                    ScrollBar.Trigger();
                    Selection.MoveUp();
                }
                if ( InputSequenceManager.Keyboard.IsActionTriggered( Actions.Down ) )
                {
                    ScrollBar.Trigger();
                    Selection.MoveDown();
                }
                if ( InputSequenceManager.Keyboard.IsActionTriggered( Actions.Esc ) )
                    Selection.Clear();
                //-----------------------------------------------------------------
                if ( InputSequenceManager.Keyboard.CtrlDown &&
                    InputSequenceManager.Keyboard.IsActionTriggered( Actions.CreateItem ) )
                {
                    AddItem();
                }
                if ( View.IsEnabled( ViewState.View_Has_Selection ) )
                {
                    if ( InputSequenceManager.Keyboard.CtrlDown &&
                        InputSequenceManager.Keyboard.IsActionTriggered( Actions.CreateChildItem ) )
                    {
                        AddChildItem();
                    }
                }
            }
        }

        public override void UpdateStrucutre( GameTime gameTime )
        {
            base.UpdateStrucutre( gameTime );

            if ( Region.IsEnabled( RegionState.Region_Hightlighted ) )
                View.Enable( ViewState.View_Has_Focus );

            ScrollBar.Upadte( gameTime );
            Region.Update( gameTime );
        }

        #endregion Update

        #region Operations

        public virtual void AddItem()
        {
            View.Items.Add( new ViewItemExchangable( View, ViewSettings, ItemSettings, itemFactory ) );
        }

        private void AddChildItem()
        {
        }

        #endregion Operations
    }
}