using MetaMind.Engine.Guis.Widgets.Items;
using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Guis.Widgets.ViewItems
{
    public interface IViewItemControl2D : IViewItemControl1D
    {
        int Row { get; set; }
    }

    public class ViewItemControl2D : ViewItemComponent, IViewItemControl2D
    {
        //---------------------------------------------------------------------
        private readonly ViewItemFrameControl defaultFrameControl;
        
        //---------------------------------------------------------------------
        private int column;
        private int id;
        private int row;

        #region Constructors

        public ViewItemControl2D(IViewItem item)
            : base(item)
        {
            defaultFrameControl = new ViewItemFrameControl(item);
        }

        #endregion

        #region Public Properties

        public int Column
        {
            get { return column; }
            set { column = value; }
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

        public int Row
        {
            get { return row; }
            set { row = value; }
        }

        #endregion

        #region Update

        public void Update( GameTime gameTime )
        {
            UpdateViewScroll();
            UpdateViewSelection();
            UpdateViewSwap();

            defaultFrameControl.Update( gameTime );
        }

        private void UpdateViewScroll()
        {
            Id     = View.Items  .IndexOf( Item );
            Row    = View.Control.RowFrom( Id );
            Column = View.Control.ColumnFrom( Id );

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
            this        .ViewControl.Swap.Initialize( originCenter, targetCenter );

            ProcessManager.AttachProcess( new ViewItemSwapProcess( draggingItem, Item ) );
        }
        public void UnSelectIt()
        {
            if ( ViewControl.Selection.IsSelected( Id ) )
                ViewControl.Selection.Clear();
        }

        #endregion Operations
    }
}