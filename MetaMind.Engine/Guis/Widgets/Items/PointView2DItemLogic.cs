
namespace MetaMind.Engine.Guis.Widgets.Items
{
    using MetaMind.Engine.Guis.Widgets.Items.ItemData;
    using MetaMind.Engine.Guis.Widgets.Items.ItemFrames;
    using MetaMind.Engine.Guis.Widgets.Items.ItemView;
    using MetaMind.Engine.Guis.Widgets.Views.PointView.ViewItem;

    public class PointView2DItemLogic : ViewItemLogic, IPointView2DItemLogic
    {
        #region Constructors

        // TODO: Injdection problems

        public PointView2DItemLogic(IViewItem item, dynamic itemFrameControl, dynamic itemViewControl, dynamic itemDataControl)
            : base(item, (object)itemFrameControl, (object)itemViewControl, (object)itemDataControl)
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