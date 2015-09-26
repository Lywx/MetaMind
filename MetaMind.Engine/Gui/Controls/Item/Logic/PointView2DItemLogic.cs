
namespace MetaMind.Engine.Gui.Controls.Item.Logic
{
    using Data;
    using Frames;
    using Interactions;
    using Layouts;

    public class PointView2DItemLogic : ViewItemLogic, IPointView2DItemLogic 
    {
        #region Constructors

        public PointView2DItemLogic(IViewItem item, IViewItemFrameController itemFrame, IViewItemInteraction itemInteraction, IViewItemDataModel itemModel, IViewItemLayout itemLayout)
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