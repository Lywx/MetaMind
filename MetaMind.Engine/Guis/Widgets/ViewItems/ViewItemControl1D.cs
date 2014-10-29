using MetaMind.Engine.Guis.Widgets.Items;
using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Guis.Widgets.ViewItems
{
    public class ViewItemControl1D : ViewItemComponent
    {
        private readonly ViewItemViewControl1D itemViewControl;
        private readonly ViewItemDataControl   itemDataControl;

        #region Constructors

        public ViewItemControl1D( IViewItem item )
            : base(item)
        {
            ItemFrameControl = new ViewItemFrameControl( item );
            itemViewControl  = new ViewItemViewControl1D( item );
            itemDataControl  = new ViewItemDataControl( item );
        }

        #endregion

        #region Public Properties

        public int Id { get; set; }

        public IItemRootFrame RootFrame
        {
            get { return ItemFrameControl.RootFrame; }
        }

        protected ViewItemFrameControl ItemFrameControl { get; set; }

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

        public void EditIt()
        {
            itemDataControl.EditLabel( ViewItemLabelType.Name );
        }

        #endregion Operations

        #region Update

        public virtual void Update( GameTime gameTime )
        {
            itemViewControl .Update( gameTime );
            ItemFrameControl.Update( gameTime );
        }

        #endregion Update
    }
}