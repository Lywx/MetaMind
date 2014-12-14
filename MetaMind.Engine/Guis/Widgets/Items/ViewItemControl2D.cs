
namespace MetaMind.Engine.Guis.Widgets.Items
{
    public class ViewItemControl2D : ViewItemControl1D
    {
        #region Constructors

        public ViewItemControl2D(IViewItem item)
            : base(item)
        {
            this.ItemViewControl  = new ViewItemViewControl2D(item);
            this.ItemFrameControl = new ViewItemFrameControl(item);
            this.ItemDataControl  = new ViewItemDataControl(item);
        }


        #endregion Constructors

        #region Public Properties

        public int           Column    { get; set; }
        public int           Row       { get; set; }

        #endregion Public Properties
    }
}