using MetaMind.Engine.Components.Inputs;
using MetaMind.Engine.Guis.Widgets.Items;
using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Guis.Widgets.ViewItems
{
    public class ViewItemControl1D : ViewItemComponent
    {
        //---------------------------------------------------------------------
        protected dynamic             ItemViewControl  { get; set; }
        protected dynamic             ItemFrameControl { get; set; }
        protected ViewItemDataControl ItemDataControl  { get; set; }

        //---------------------------------------------------------------------
        #region Constructors

        public ViewItemControl1D( IViewItem item )
            : base( item )
        {
            ItemFrameControl = new ViewItemFrameControl( item );
            ItemViewControl  = new ViewItemViewControl1D( item );
            ItemDataControl  = new ViewItemDataControl( item );
        }

        #endregion Constructors

        #region Public Properties

        public int Id { get; set; }

        public IItemRootFrame RootFrame
        {
            get { return ItemFrameControl.RootFrame; }
        }

        #endregion Public Properties

        #region Operations

        public virtual void SelectIt()
        {
            ItemViewControl.SelectIt();
        }

        public virtual void SwapIt( IViewItem draggingItem )
        {
            ItemViewControl.SwapIt( draggingItem );
        }

        public virtual void UnselectIt()
        {
            ItemViewControl.UnselectIt();
        }

        #endregion Operations

        #region Update

        public virtual void UpdateInput( GameTime gameTime )
        {
            // mouse
            ItemFrameControl.UpdateInput( gameTime );
            // keyboard
            if ( Item.IsEnabled( ItemState.Item_Selected ) &&
                !Item.IsEnabled( ItemState.Item_Editing ) )
            {
            }
        }

        public virtual void UpdateStructure( GameTime gameTime )
        {
            ItemViewControl .UpdateStructure( gameTime );
            ItemFrameControl.UpdateStructure( gameTime );
            ItemDataControl .UpdateStructure( gameTime );
        }
        #endregion Update
    }
}