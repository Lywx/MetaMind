using MetaMind.Engine.Guis.Widgets.Items;
using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Guis.Widgets.ViewItems
{
    public class ViewItemViewControl1D : ViewItemComponent
    {
        public ViewItemViewControl1D( IViewItem item )
            : base( item )
        {
        }

        public void SelectIt()
        {
            ViewControl.Selection.Select( ItemControl.Id );
        }

        public void SwapIt( IViewItem draggingItem )
        {
            if ( Item.IsEnabled( ItemState.Item_Swaping ) )
                return;
            else
                Item.Enable( ItemState.Item_Swaping );

            var originCenter = this        .ViewControl.Scroll.RootCenterPoint( this        .ItemControl.Id );
            var targetCenter = draggingItem.ViewControl.Scroll.RootCenterPoint( draggingItem.ItemControl.Id );
            this                           .ViewControl.Swap.Initialize( originCenter, targetCenter );

            ProcessManager.AttachProcess( new ViewItemSwapProcess( draggingItem, Item ) );
        }

        public void UnSelectIt()
        {
            if ( ViewControl.Selection.IsSelected( ItemControl.Id ) )
                ViewControl.Selection.Clear();
        }

        protected virtual void UpdateViewScroll()
        {
            ItemControl.Id = View.Items.IndexOf( Item );

            // dynamic binding here
            if ( ViewControl.Scroll.CanDisplay( ItemControl.Id ) )
            {
                Item.Enable( ItemState.Item_Active );
            }
            else
            {
                Item.Disable( ItemState.Item_Active );
            }
        }

        private void UpdateViewSelection()
        {
            if ( ViewControl.Selection.IsSelected( ItemControl.Id ) )
                Item.Enable( ItemState.Item_Selected );
            else
                Item.Disable( ItemState.Item_Selected );
        }

        private void UpdateViewSwap()
        {
            if (Item.IsEnabled( ItemState.Item_Dragging ) )
                foreach ( var observor in ViewControl.Swap.Observors )
                    ViewControl.Swap.ObserveSwapFrom(Item, observor );
        }

        public void Update( GameTime gameTime )
        {
            UpdateViewScroll();
            UpdateViewSelection();
            UpdateViewSwap();
        }
    }
}