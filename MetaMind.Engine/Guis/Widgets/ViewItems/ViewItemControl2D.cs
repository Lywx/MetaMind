
namespace MetaMind.Engine.Guis.Widgets.ViewItems
{
    public class ViewItemControl2D : ViewItemControl1D
    {
        #region Constructors

        public ViewItemControl2D( IViewItem item ) 
            : base( item )
        {
            ItemViewControl  = new ViewItemViewControl2D( item );
            ItemFrameControl = new ViewItemFrameControl( item );
            ItemDataControl  = new ViewItemDataControl( item );
        }


        #endregion Constructors

        #region Public Properties

        public int           Column    { get; set; }
        public int           Row       { get; set; }

        #endregion Public Properties
    }
}