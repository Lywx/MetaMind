namespace MetaMind.Engine.Guis.Elements.Views
{
    using System;

    using MetaMind.Engine.Components.Inputs;

    using Microsoft.Xna.Framework;

    public class ViewControl2D : ViewControl1D
    {
        public ViewControl2D( IView view, ViewSettings2D viewSettings, ICloneable itemSettings )
            : base( view, viewSettings, itemSettings )
        {
            this.Swap      = new ViewSwapControl       ( this.View, this.ViewSettings, this.ItemSettings );
            this.Scroll    = new ViewScrollControl2D   ( this.View, this.ViewSettings, this.ItemSettings );
            this.Selection = new ViewSelectionControl2D( this.View, this.ViewSettings, this.ItemSettings );
        }

        #region Update

        public override void UpdateInput( GameTime gameTime )
        {
            if ( this.AcceptInput )
            {
                // mouse
                //---------------------------------------------------------------------
                if ( this.InputSequenceManager.Mouse.IsWheelScrolledUp )
                {
                    this.Scroll.MoveUp();
                }
                if ( this.InputSequenceManager.Mouse.IsWheelScrolledDown )
                {
                    this.Scroll.MoveDown();
                }

                // keyboard
                //--------------------------------------------------------------
                // movement 
                if ( this.InputSequenceManager.Keyboard.IsActionTriggered( Actions.Left ) )
                {
                    this.Selection.MoveLeft();
                }
                if ( this.InputSequenceManager.Keyboard.IsActionTriggered( Actions.Right ) )
                {
                    this.Selection.MoveRight();
                }
                if ( this.InputSequenceManager.Keyboard.IsActionTriggered( Actions.Up ) )
                {
                    this.Selection.MoveUp();
                }
                if ( this.InputSequenceManager.Keyboard.IsActionTriggered( Actions.Down ) )
                {
                    this.Selection.MoveDown();
                }
                // escape
                if ( this.InputSequenceManager.Keyboard.IsActionTriggered( Actions.Escape ) )
                {
                    this.Selection.Clear();
                }
            }

            // item input
            //-----------------------------------------------------------------
            foreach ( var item in this.View.Items.ToArray() )
            {
                item.UpdateInput( gameTime );
            }
        }
       
        #endregion Update

        #region Helper Methods

        public int ColumnNum
        {
            get { return this.RowNum > 1 ? this.ViewSettings.ColumnNumMax : this.View.Items.Count; }
        }

        public int RowNum
        {
            get
            {
                var lastId = this.View.Items.Count - 1;
                return this.RowFrom( lastId ) + 1;
            }
        }

        public int ColumnFrom( int id )
        {
            return id % this.ViewSettings.ColumnNumMax;
        }

        public int IdFrom( int i, int j )
        {
            return i * this.ViewSettings.ColumnNumMax + j;
        }

        public int RowFrom( int id )
        {
            for ( var row = 0 ; row < this.ViewSettings.RowNumMax ; row++ )
            {
                if ( id - row * this.ViewSettings.ColumnNumMax >= 0 )
                    continue;
                return row - 1;
            }
            return this.ViewSettings.RowNumMax - 1;
        }
        #endregion Helper Methods
    }
}