using System;
using System.Linq;
using MetaMind.Engine.Components.Inputs;
using MetaMind.Engine.Guis.Widgets.Items;
using MetaMind.Engine.Settings;
using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Guis.Widgets.Views
{
    public class ViewControl2D : ViewComponent, IViewControl
    {
        public ViewControl2D( IView view, ICloneable viewSettings, ICloneable itemSettings )
            : base( view, viewSettings, itemSettings )
        {
            Swap      = new ViewSwapControl( View, ViewSettings, ItemSettings );
            Scroll    = new ViewScrollControl2D( View, ViewSettings, ItemSettings );
            Selection = new ViewSelectionControl2D( View, ViewSettings, ItemSettings );
        }

        #region Components

        public IViewSwapControl Swap { get; private set; }
        public dynamic Scroll { get; private set; }
        public dynamic Selection { get; private set; }

        #endregion Components

        #region Operations

        public void SortItems( ViewSortMode sortMode )
        {
            switch ( sortMode )
            {
                case ViewSortMode.Name:
                {
                    View.Items = View.Items.OrderBy( item => item.ItemData.Labels ).ToList();
                    View.Items.ForEach( item => item.ItemControl.Id = View.Items.IndexOf( item ) );
                }
                    break;

                case ViewSortMode.Id:
                {
                    View.Items = View.Items.OrderBy( item => item.ItemControl.Id ).ToList();
                    View.Items.ForEach( item => item.ItemControl.Id = View.Items.IndexOf( item ) );
                }
                    break;
            }
        }

        #endregion Operations

        #region Update

        public virtual void UpdateInput( GameTime gameTime )
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
                    Selection.MoveLeft();
                if ( InputSequenceManager.Keyboard.IsActionTriggered( Actions.Right ) )
                    Selection.MoveRight();
                if ( InputSequenceManager.Keyboard.IsActionTriggered( Actions.Up) )
                    Selection.MoveUp();
                if ( InputSequenceManager.Keyboard.IsActionTriggered( Actions.Down) )
                    Selection.MoveDown();
                if ( InputSequenceManager.Keyboard.IsActionTriggered( Actions.Esc ) )
                    Selection.Clear();
            }
        }

        public virtual void UpdateStrucutre( GameTime gameTime )
        {
            UpdateViewLogics();

            UpdateItems( gameTime );
        }

        private void UpdateViewLogics()
        {
            if ( Selection.HasSelected )
            {
                View.Enable( ViewState.View_Has_Selection );
            }
            else
            {
                View.Disable( ViewState.View_Has_Selection );
            }

            if ( View.IsEnabled( ViewState.View_Has_Selection ) )
            {
                View.Enable( ViewState.View_Has_Focus );
            }
            else
            {
                View.Disable( ViewState.View_Has_Focus );
            }
        }

        private void UpdateItems( GameTime gameTime )
        {
            foreach ( var item in View.Items )
            {
                item.Update( gameTime );
            }
        }

        #endregion Update
        
        #region Helper Methods
        
        public int ColumnFrom( int id )
        {
            return id % ViewSettings.ColumnNumMax;
        }

        public int IdFrom( int i, int j )
        {
            return i * ViewSettings.ColumnNumMax + j;
        }

        public int RowFrom( int id )
        {
            for ( var row = 0 ; row < ViewSettings.RowNumMax ; row++ )
            {
                if ( id - row * ViewSettings.ColumnNumMax >= 0 )
                    continue;
                return row - 1;
            }
            return ViewSettings.RowNumMax - 1;
        }

        public int RowNum
        {
            get
            {
                var lastId = View.Items.Count - 1;
                return RowFrom( lastId ) + 1;
            }
        }

        public int ColumnNum
        {
            get { return RowNum > 1 ? ViewSettings.ColumnNumMax : View.Items.Count; }
        }

        #endregion
    }
}