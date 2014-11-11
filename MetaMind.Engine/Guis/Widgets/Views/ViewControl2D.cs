using System;
using System.Linq;
using MetaMind.Engine.Components.Inputs;
using MetaMind.Engine.Guis.Widgets.Items;
using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Guis.Widgets.Views
{
    public class ViewControl2D : ViewControl1D
    {
        public ViewControl2D( IView view, ViewSettings2D viewSettings, ICloneable itemSettings )
            : base( view, viewSettings, itemSettings )
        {
            Swap      = new ViewSwapControl       ( View, ViewSettings, ItemSettings );
            Scroll    = new ViewScrollControl2D   ( View, ViewSettings, ItemSettings );
            Selection = new ViewSelectionControl2D( View, ViewSettings, ItemSettings );
        }

        #region Update

        public override void UpdateInput( GameTime gameTime )
        {
            if ( AcceptInput )
            {
                // mouse
                //---------------------------------------------------------------------
                if ( InputSequenceManager.Mouse.IsWheelScrolledUp )
                {
                    Scroll.MoveUp();
                }
                if ( InputSequenceManager.Mouse.IsWheelScrolledDown )
                {
                    Scroll.MoveDown();
                }

                // keyboard
                //--------------------------------------------------------------
                // movement 
                if ( InputSequenceManager.Keyboard.IsActionTriggered( Actions.Left ) )
                {
                    Selection.MoveLeft();
                }
                if ( InputSequenceManager.Keyboard.IsActionTriggered( Actions.Right ) )
                {
                    Selection.MoveRight();
                }
                if ( InputSequenceManager.Keyboard.IsActionTriggered( Actions.Up ) )
                {
                    Selection.MoveUp();
                }
                if ( InputSequenceManager.Keyboard.IsActionTriggered( Actions.Down ) )
                {
                    Selection.MoveDown();
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
       
        #endregion Update

        #region Helper Methods

        public int ColumnNum
        {
            get { return RowNum > 1 ? ViewSettings.ColumnNumMax : View.Items.Count; }
        }

        public int RowNum
        {
            get
            {
                var lastId = View.Items.Count - 1;
                return RowFrom( lastId ) + 1;
            }
        }

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
        #endregion Helper Methods
    }
}