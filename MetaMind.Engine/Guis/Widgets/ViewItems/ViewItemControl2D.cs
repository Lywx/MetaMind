using MetaMind.Engine.Guis.Widgets.Items;
using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Guis.Widgets.ViewItems
{
    public class ViewItemControl2D : ViewItemComponent
    {
        //---------------------------------------------------------------------
        private readonly ViewItemFrameControl  itemFrameControl;
        private readonly ViewItemViewControl2D itemViewControl;

        //---------------------------------------------------------------------

        #region Constructors

        public ViewItemControl2D( IViewItem item )
            : base( item )
        {
            itemFrameControl = new ViewItemFrameControl( item );
            itemViewControl = new ViewItemViewControl2D( item );
        }

        #endregion Constructors

        #region Public Properties

        public int Column { get; set; }

        public int Id { get; set; }

        public IItemRootFrame RootFrame
        {
            get { return itemFrameControl.RootFrame; }
        }

        public int Row { get; set; }

        #endregion Public Properties

        #region Operations

        public void SelectIt()
        {
            itemViewControl.SelectIt();
        }

        public void SwapIt( IViewItem draggingItem )
        {
            itemViewControl.SwapIt( draggingItem );
        }

        public void UnSelectIt()
        {
            itemViewControl.UnSelectIt();
        }

        #endregion Operations

        #region Update

        public void Update( GameTime gameTime )
        {
            itemViewControl.Update( gameTime );
            itemFrameControl.Update( gameTime );
        }

        #endregion Update
    }
}