namespace MetaMind.Engine.Guis.Elements.ViewItems
{
    using MetaMind.Engine.Guis.Elements.Items;

    using Microsoft.Xna.Framework;

    public class ViewItemViewControl1D : ViewItemComponent
    {
        public ViewItemViewControl1D( IViewItem item )
            : base( item )
        {
        }

        public void SelectIt()
        {
            this.ViewControl.Selection.Select( this.ItemControl.Id );
        }

        public void SwapIt( IViewItem draggingItem )
        {
            if ( this.Item.IsEnabled( ItemState.Item_Swaping ) )
            {
                return;
            }

            this.Item.Enable( ItemState.Item_Swaping );

            var originCenter = this        .ViewControl.Scroll.RootCenterPoint( this        .ItemControl.Id );
            var targetCenter = draggingItem.ViewControl.Scroll.RootCenterPoint( draggingItem.ItemControl.Id );

                               this        .ViewControl.Swap  .Initialize( originCenter, targetCenter );

            this.ProcessManager.AttachProcess( new ViewItemSwapProcess( draggingItem, this.Item ) );
        }

        public void UnselectIt()
        {
            if ( this.ViewControl.Selection.IsSelected( this.ItemControl.Id ) )
            {
                this.ViewControl.Selection.Clear();
            }
        }

        protected virtual void UpdateViewScroll()
        {
            this.ItemControl.Id = this.View.Items.IndexOf( this.Item );

            // dynamic binding here
            if ( this.ViewControl.Scroll.CanDisplay( this.ItemControl.Id ) )
            {
                this.Item.Enable( ItemState.Item_Active );
            }
            else
            {
                this.Item.Disable( ItemState.Item_Active );
            }
        }

        private void UpdateViewSelection()
        {
            if ( this.ViewControl.Selection.IsSelected( this.ItemControl.Id ) )
            {
                // unify mouse and keyboard selection
                // not sure whether fired only once
                if ( !this.Item.IsEnabled( ItemState.Item_Selected ) )
                {
                    this.ItemControl.SelectIt();
                }
                this.Item.Enable( ItemState.Item_Selected );
            }
            else
            {
                // unify mouse and keyboard selection
                // not sure whether fired only once
                if ( this.Item.IsEnabled( ItemState.Item_Selected ) )
                {
                    this.ItemControl.UnselectIt();
                }
                this.Item.Disable( ItemState.Item_Selected );
            }
        }
        private void UpdateViewSwap()
        {
            if ( this.Item.IsEnabled( ItemState.Item_Dragging ) )
            {
                foreach ( var observor in this.ViewControl.Swap.Observors )
                {
                    this.ViewControl.Swap.ObserveSwapFrom( this.Item, observor );
                }
            }
        }

        public void UpdateStructure( GameTime gameTime )
        {
            this.UpdateViewScroll();
            this.UpdateViewSelection();
            this.UpdateViewSwap();
        }
    }
}