namespace MetaMind.Engine.Gui.Control.Item.Logic
{
    using Data;
    using Frames;
    using Interactions;
    using Layouts;

    public class PointViewHorizontalItemLogic : ViewItemLogic, IPointViewHorizontalItemLogic
    {
        public PointViewHorizontalItemLogic(IViewItem item, IViewItemFrameController itemFrame, IViewItemInteraction itemInteraction, IViewItemDataModel itemModel, IViewItemLayout itemLayout) : base(item, itemFrame, itemInteraction, itemModel, itemLayout)
        {
        }

        public new IPointViewItemLayout ItemLayout
        {
            get { return (IPointViewItemLayout)base.ItemLayout; }
        }
    }
}