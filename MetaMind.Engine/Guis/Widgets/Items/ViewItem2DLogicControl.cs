
namespace MetaMind.Engine.Guis.Widgets.Items
{
    public class ViewItem2DLogicControl : ViewItem1DLogicControl
    {
        #region Constructors

        public ViewItem2DLogicControl(IViewItem item)
            : base(item)
        {
            this.ItemViewControl  = new ViewItemView2DControl(item);
            this.ItemFrameControl = new ViewItemFrameControl(item);
            this.ItemDataControl  = new ViewItemDataModifier(item);
        }


        #endregion Constructors

        #region Public Properties

        public int Column { get; set; }

        public int Row { get; set; }

        #endregion Public Properties
    }
}