using MetaMind.Engine.Guis.Widgets.Items;
using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Guis.Widgets.ViewItems
{
    public class ViewItemControl1D : ViewItemComponent
    {
        private readonly ViewItemFrameControl  frameControl;
        private readonly ViewItemViewControl1D viewControl;
        private readonly ViewItemDataControl   dataControl;

        #region Constructors

        public ViewItemControl1D(IViewItem item)
            : base(item)
        {
            frameControl = new ViewItemFrameControl( item );
            viewControl  = new ViewItemViewControl1D( item );
            dataControl  = new ViewItemDataControl( item );
        }

        #endregion

        #region Public Properties

        public int Id { get; set; }

        public IItemRootFrame RootFrame
        {
            get { return frameControl.RootFrame; }
        }

        #endregion Public Properties

        #region Operations

        public void SelectIt()
        {
            viewControl.SelectIt();
        }

        public void SwapIt( IViewItem draggingItem )
        {
            viewControl.SwapIt( draggingItem );
        }

        public void UnSelectIt()
        {
            viewControl.UnSelectIt();
        }

        public void EditIt()
        {
            dataControl.EditLabel();
        }

        #endregion Operations

        #region Update

        public virtual void Update( GameTime gameTime )
        {
            viewControl .Update( gameTime );
            frameControl.Update( gameTime );
        }

        #endregion Update
    }
}