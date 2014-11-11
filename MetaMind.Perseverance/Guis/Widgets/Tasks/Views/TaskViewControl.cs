using MetaMind.Engine.Components.Inputs;
using MetaMind.Engine.Guis.Elements.Regions;
using MetaMind.Engine.Guis.Widgets.ViewItems;
using MetaMind.Engine.Guis.Widgets.Views;
using MetaMind.Perseverance.Concepts.TaskEntries;
using MetaMind.Perseverance.Guis.Widgets.Tasks.Items;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis.Widgets.Tasks.Views
{
    public class TaskViewControl : ViewControl2D
    {
        #region Constructors
        public TaskViewControl( IView view, TaskViewSettings viewSettings, TaskItemSettings itemSettings )
            : base( view, viewSettings, itemSettings )
        {
            Region      = new ViewRegion( view, viewSettings, itemSettings, RegionPositioning );
            ScrollBar   = new TaskViewScrollBar( view, viewSettings, itemSettings, viewSettings.ScrollBarSettings );
            ItemFactory = new TaskItemFactory();
        }

        private Rectangle RegionPositioning( dynamic viewSettings, dynamic itemSettings )
        {
            return new Rectangle(
                viewSettings.StartPoint.X,
                viewSettings.StartPoint.Y,
                viewSettings.ColumnNumDisplay * ( itemSettings.NameFrameSize.X ),
                viewSettings.RowNumDisplay    * ( itemSettings.NameFrameSize.Y + itemSettings.IdFrameSize.Y )
                );
        }

        #endregion Constructors

        #region Public Properties

        public TaskItemFactory   ItemFactory { get; protected set; }
        public ViewRegion        Region      { get; protected set; }
        public TaskViewScrollBar ScrollBar   { get; protected set; }

        #endregion Public Properties

        #region Update

        public override void UpdateInput( GameTime gameTime )
        {
            // mouse
            //-----------------------------------------------------------------
            // region
            Region.UpdateInput( gameTime );

            if ( AcceptInput )
            {
                // mouse
                //---------------------------------------------------------------------
                // scroll
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

                // keyboard
                //---------------------------------------------------------------------
                // movement
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
                    for ( var i = 0 ; i < ViewSettings.RowNumDisplay ; i++ )
                    {
                        MoveUp();
                    }
                }
                if ( InputSequenceManager.Keyboard.IsActionTriggered( Actions.SDown ) )
                {
                    for ( var i = 0 ; i < ViewSettings.RowNumDisplay ; i++ )
                    {
                        MoveDown();
                    }
                }
                // escape
                if ( InputSequenceManager.Keyboard.IsActionTriggered( Actions.Escape ) )
                {
                    Selection.Clear();
                }                
                // list management
                if ( InputSequenceManager.Keyboard.IsActionTriggered( Actions.TaskCreateItem ) )
                {
                    AddItem();
                }
            }

            // item input
            //-----------------------------------------------------------------
            foreach ( var item in View.Items.ToArray() )
            {
                item.UpdateInput( gameTime );
            }
        }

        public override void UpdateStrucutre( GameTime gameTime )
        {
            base     .UpdateStrucutre( gameTime );
            Region   .UpdateStructure( gameTime );
            ScrollBar.Upadte( gameTime );
        }

        protected override void UpdateViewFocus()
        {
            if ( View.IsEnabled( ViewState.View_Active ) )
            {
                if ( Region.IsEnabled( RegionState.Region_Hightlighted ) )
                {
                    View.Enable( ViewState.View_Has_Focus );
                }
                else
                {
                    View.Disable( ViewState.View_Has_Focus );
                }
            }
            else
            {
                View.Disable( ( ViewState.View_Has_Focus ) );
            }
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

        public void MoveDown()
        {
            ScrollBar.Trigger();
            Selection.MoveDown();
        }

        public override void MoveLeft()
        {
            ScrollBar.Trigger();
            Selection.MoveLeft();
        }

        public override void MoveRight()
        {
            ScrollBar.Trigger();
            Selection.MoveRight();
        }

        public void MoveUp()
        {
            ScrollBar.Trigger();
            Selection.MoveUp();
        }

        #endregion Operations
    }
}