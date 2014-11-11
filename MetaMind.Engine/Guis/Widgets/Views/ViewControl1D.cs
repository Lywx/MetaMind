using MetaMind.Engine.Components.Inputs;
using MetaMind.Engine.Guis.Widgets.Items;
using Microsoft.Xna.Framework;
using System;
using System.Linq;

namespace MetaMind.Engine.Guis.Widgets.Views
{
    public class ViewControl1D : ViewComponent, IViewControl
    {
        public ViewControl1D( IView view, ViewSettings1D viewSettings, ICloneable itemSettings )
            : base( view, viewSettings, itemSettings )
        {
            Swap      = new ViewSwapControl       ( View, ViewSettings, ItemSettings );
            Scroll    = new ViewScrollControl1D   ( View, ViewSettings, ItemSettings );
            Selection = new ViewSelectionControl1D( View, ViewSettings, ItemSettings );
        }

        protected ViewControl1D( IView view, ViewSettings2D viewSettings, ICloneable itemSettings )
            : base( view, viewSettings, itemSettings )
        {
        }

        #region Components

        public dynamic          Scroll    { get; protected set; }
        public dynamic          Selection { get; protected set; }
        public IViewSwapControl Swap      { get; protected set; }

        #endregion Components

        #region Operations

        public virtual void MoveLeft()
        {
            if ( ViewSettings.Direction == ViewSettings1D.ScrollDirection.Left )
            {
                // invert for left scrolling view
                Selection.MoveRight();
            }
            else
            {
                Selection.MoveLeft();
            }
        }

        public virtual void MoveRight()
        {
            if ( ViewSettings.Direction == ViewSettings1D.ScrollDirection.Left )
            {
                // invert for left scrolling view
                Selection.MoveLeft();
            }
            else
            {
                Selection.MoveRight();
            }
        }

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

        public virtual bool AcceptInput
        {
            get
            {
                return View.IsEnabled( ViewState.View_Active ) &&
                       View.IsEnabled( ViewState.View_Has_Focus ) &&
                      !View.IsEnabled( ViewState.Item_Editting );
            }
        }

        public virtual void UpdateInput( GameTime gameTime )
        {
            if ( AcceptInput )
            {
                // mouse
                //------------------------------------------------------------------
                if ( InputSequenceManager.Mouse.IsWheelScrolledUp )
                {
                    Scroll.MoveLeft();
                }
                if ( InputSequenceManager.Mouse.IsWheelScrolledDown )
                {
                    Scroll.MoveRight();
                }

                // keyboard
                //------------------------------------------------------------------
                // movement
                if ( InputSequenceManager.Keyboard.IsActionTriggered( Actions.Left ) )
                {
                    MoveLeft();
                }
                if ( InputSequenceManager.Keyboard.IsActionTriggered( Actions.Right ) )
                {
                    MoveRight();
                }
                // escape
                if ( InputSequenceManager.Keyboard.IsActionTriggered( Actions.Escape ) )
                {
                    Selection.Clear();
                }
            }

            // item input
            //-----------------------------------------------------------------
            foreach ( var item in View.Items.ToArray() )
            {
                item.UpdateInput( gameTime );
            }
        }

        public virtual void UpdateStrucutre( GameTime gameTime )
        {
            UpdateViewLogics();
            // TODO: this name is not right
            UpdateItemLogics( gameTime );
        }

        protected virtual void UpdateItemLogics( GameTime gameTime )
        {
            if ( View.IsEnabled( ViewState.View_Active ) )
            {
                foreach ( var item in View.Items.ToArray() )
                {
                    item.UpdateStructure( gameTime );
                }
            }
            else
            {
                foreach ( var item in View.Items )
                {
                    item.Disable( ItemState.Item_Active );
                }
            }
        }

        protected virtual void UpdateViewFocus()
        {
            if ( View.IsEnabled( ViewState.View_Active ) )
            {
                if ( View.IsEnabled( ViewState.View_Has_Selection ) )
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
                View.Disable( ViewState.View_Has_Focus );
            }
        }

        protected virtual void UpdateViewLogics()
        {
            UpdateViewSelection();
            UpdateViewFocus();
        }

        protected virtual void UpdateViewSelection()
        {
            if ( View.IsEnabled( ViewState.View_Active ) )
            {
                if ( Selection.HasSelected )
                {
                    View.Enable( ViewState.View_Has_Selection );
                }
                else
                {
                    View.Disable( ViewState.View_Has_Selection );
                }
            }
            else
            {
                View.Disable( ViewState.View_Has_Selection );
            }
        }

        #endregion Update
    }
}