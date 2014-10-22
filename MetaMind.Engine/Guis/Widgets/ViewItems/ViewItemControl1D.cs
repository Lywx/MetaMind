using MetaMind.Engine.Guis.Widgets.Items;
using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Guis.Widgets.ViewItems
{
    public class ViewItemControl1D : ViewItemComponent, IViewItemControl1D
    {
        private int id;

        private readonly ViewItemFrameControl defaultFrameControl;
        
        public ViewItemControl1D( IViewItem item )
            : base( item )
        {
            defaultFrameControl = new ViewItemFrameControl( item );
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public IItemRootFrame RootFrame
        {
            get { return defaultFrameControl.RootFrame; }
        }
        
        #region Operations

        public void SelectIt()
        {
            ViewControl.Selection.Select( Id );
        }

        public void SwapIt( IViewItem draggingItem )
        {
            if ( Item.IsEnabled( ItemState.Item_Swaping ) )
                return;
            else
                Item.Enable( ItemState.Item_Swaping );

            var originCenter = this        .ViewControl.Scroll.RootCenterPoint( this        .ItemControl.Id );
            var targetCenter = draggingItem.ViewControl.Scroll.RootCenterPoint( draggingItem.ItemControl.Id );
                               this        .ViewControl.Swap  .Initialize     ( originCenter, targetCenter      );

            ProcessManager.AttachProcess( new ViewItemSwapProcess( draggingItem, Item ) );
        }
        public void UnSelectIt()
        {
            if ( ViewControl.Selection.IsSelected( Id ) )
                ViewControl.Selection.Clear();
        }

        #endregion Operations

        #region Update

        public virtual void Update( GameTime gameTime )
        {
            UpdateViewScroll();
            UpdateViewSelection();
            UpdateViewSwap();

            defaultFrameControl.Update( gameTime );
        }

        private void UpdateViewScroll()
        {
            Id = View.Items.IndexOf( Item );

            if ( ViewControl.Scroll.CanDisplay( Id ) )
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
            if ( ViewControl.Selection.IsSelected( Id ) )
            {
                Item.Enable( ItemState.Item_Selected );
            }
            else
            {
                Item.Disable( ItemState.Item_Selected );
            }
        }

        private void UpdateViewSwap()
        {
            if ( Item.IsEnabled( ItemState.Item_Dragging ) )
            {
                foreach ( var observor in ViewControl.Swap.Observors )
                {
                    ViewControl.Swap.ObserveSwapFrom( Item, observor );
                }
            }
        }

        #endregion Update
    }
}