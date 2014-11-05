using MetaMind.Engine.Guis.Widgets.Items;
using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Guis.Widgets.ViewItems
{
    public class ViewItemControl1D : ViewItemComponent
    {
        protected ViewItemViewControl1D ItemViewControl { get; set; }

        #region Constructors

        public ViewItemControl1D( IViewItem item )
            : base( item )
        {
            ItemFrameControl = new ViewItemFrameControl( item );
            ItemViewControl = new ViewItemViewControl1D( item );
            ItemDataControl = new ViewItemDataControl( item );
        }

        #endregion Constructors

        #region Public Properties

        public int Id { get; set; }

        public IItemRootFrame RootFrame
        {
            get { return ItemFrameControl.RootFrame; }
        }

        protected ViewItemDataControl ItemDataControl { get; set; }

        protected dynamic ItemFrameControl { get; set; }

        #endregion Public Properties

        #region Operations
        
        public void SelectIt()
        {
            ItemViewControl.SelectIt();
        }

        public void SwapIt( IViewItem draggingItem )
        {
            ItemViewControl.SwapIt( draggingItem );
        }

        public void UnSelectIt()
        {
            ItemViewControl.UnSelectIt();
        }

        #endregion Operations

        #region Update

        public virtual void Update( GameTime gameTime )
        {
            ItemViewControl .Update( gameTime );
            ItemFrameControl.Update( gameTime );
        }

        #endregion Update
    }
}