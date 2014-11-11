namespace MetaMind.Engine.Guis.Elements.ViewItems
{
    using MetaMind.Engine.Guis.Elements.Items;

    using Microsoft.Xna.Framework;

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
            this.ItemFrameControl = new ViewItemFrameControl( item );
            this.ItemViewControl  = new ViewItemViewControl1D( item );
            this.ItemDataControl  = new ViewItemDataControl( item );
        }

        #endregion Constructors

        #region Public Properties

        public int Id { get; set; }

        public IItemRootFrame RootFrame
        {
            get { return this.ItemFrameControl.RootFrame; }
        }

        #endregion Public Properties

        #region Operations

        public virtual void SelectIt()
        {
            this.ItemViewControl.SelectIt();
        }

        public virtual void SwapIt( IViewItem draggingItem )
        {
            this.ItemViewControl.SwapIt( draggingItem );
        }

        public virtual void UnselectIt()
        {
            this.ItemViewControl.UnselectIt();
        }

        #endregion Operations

        #region Update

        public virtual void UpdateInput( GameTime gameTime )
        {
            // mouse
            this.ItemFrameControl.UpdateInput( gameTime );
            // keyboard
            if ( this.Item.IsEnabled( ItemState.Item_Selected ) &&
                !this.Item.IsEnabled( ItemState.Item_Editing ) )
            {
            }
        }

        public virtual void UpdateStructure( GameTime gameTime )
        {
            this.ItemViewControl .UpdateStructure( gameTime );
            this.ItemFrameControl.UpdateStructure( gameTime );
            this.ItemDataControl .UpdateStructure( gameTime );
        }
        #endregion Update
    }
}