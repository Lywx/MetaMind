
namespace MetaMind.Engine.Guis.Widgets.Items.Logic
{
    using MetaMind.Engine.Guis.Widgets.Items.Data;
    using MetaMind.Engine.Guis.Widgets.Items.Frames;
    using MetaMind.Engine.Guis.Widgets.Items.Views;

    public class PointView2DItemLogic : ViewItemLogic, IPointView2DItemLogic
    {
        #region Constructors

        // TODO: Injdection problems

        public PointView2DItemLogic(IViewItem item, PointView2DItemViewControl itemView)
            : base(item, itemView)
        {
            this.ItemFrame = new ViewItemFrameControl(item);
            this.ItemDataControl  = new ViewItemDataModifier(item);
        }

        #endregion Constructors

        #region Public Properties

        public int Column { get; set; }

        public int Row { get; set; }

        #endregion Public Properties
    }
}