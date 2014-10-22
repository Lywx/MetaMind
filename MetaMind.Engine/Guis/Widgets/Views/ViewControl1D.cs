using System;
using System.Linq;
using MetaMind.Engine.Components.Inputs;
using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Guis.Widgets.Views
{
    public class ViewControl1D : ViewComponent, IViewControl
    {
        public ViewControl1D( IView view, ICloneable viewSettings, ICloneable itemSettings )
            : base( view, viewSettings, itemSettings )
        {
            Swap = new ViewSwapControl( View, ViewSettings, ItemSettings );
            Scroll = new ViewScrollControl1D( View, ViewSettings, ItemSettings );
            Selection = new ViewSelectionControl1D( View, ViewSettings, ItemSettings );
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

        public void UpdateInput( GameTime gameTime )
        {
            if ( View.IsEnabled( ViewState.View_Active ) &&
                View.IsEnabled( ViewState.View_Has_Focus ) &&
                !View.IsEnabled( ViewState.Item_Editting ) )
            {
                //------------------------------------------------------------------
                // mouse
                if ( InputSequenceManager.Mouse.IsWheelScrolledUp )
                    Scroll.MoveLeft();
                if ( InputSequenceManager.Mouse.IsWheelScrolledDown )
                    Scroll.MoveRight();

                //------------------------------------------------------------------
                // keyboard
                if ( InputSequenceManager.Keyboard.IsActionTriggered( Actions.Left ) )
                    Selection.MoveLeft();
                if ( InputSequenceManager.Keyboard.IsActionTriggered( Actions.Right ) )
                    Selection.MoveRight();
                if ( InputSequenceManager.Keyboard.IsActionTriggered( Actions.Esc ) )
                    Selection.Clear();
            }
        }

        public void UpdateStrucutre( GameTime gameTime )
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
    }
}