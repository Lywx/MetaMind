
namespace MetaMind.Engine.Guis.Widgets.Items.Logic
{
    using Layouts;
    using MetaMind.Engine.Guis.Widgets.Items.Data;
    using MetaMind.Engine.Guis.Widgets.Items.Frames;
    using MetaMind.Engine.Guis.Widgets.Items.Interactions;

    public class PointView2DItemLogic : ViewItemLogic, IPointView2DItemLogic 
    {
        #region Constructors

        public PointView2DItemLogic(IViewItem item, IViewItemFrame itemFrame, IViewItemInteraction itemInteraction, IViewItemDataModel itemModel, IViewItemLayout itemLayout)
            : base(item, itemFrame, itemInteraction, itemModel, itemLayout)
        {
        }

        #endregion Constructors

        public new IPointViewItemLayout ItemLayout
        {
            get { return (IPointViewItemLayout)base.ItemLayout; }
        }
    }
}