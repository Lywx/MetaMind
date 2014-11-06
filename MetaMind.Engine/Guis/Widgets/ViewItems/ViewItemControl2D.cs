using MetaMind.Engine.Guis.Widgets.Items;
using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Guis.Widgets.ViewItems
{
    public class ViewItemControl2D : ViewItemComponent
    {
        //---------------------------------------------------------------------
        public ViewItemFrameControl ItemFrameControl { get; protected set; }
        public ViewItemViewControl2D ItemViewControl { get; protected set; }

        //---------------------------------------------------------------------

        #region Constructors

        public ViewItemControl2D( IViewItem item )
            : base( item )
        {
            ItemFrameControl = new ViewItemFrameControl( item );
            ItemViewControl  = new ViewItemViewControl2D( item );
        }

        #endregion Constructors

        #region Public Properties

        public int Column { get; set; }

        public int Id { get; set; }

        public ItemRootFrame RootFrame
        {
            get { return ItemFrameControl.RootFrame; }
        }

        public int Row { get; set; }

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

        public void Update( GameTime gameTime )
        {
            ItemViewControl.Update( gameTime );
            ItemFrameControl.Update( gameTime );
        }

        #endregion Update
    }
}