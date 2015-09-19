namespace MetaMind.Engine.Gui.Control.Item.Layers
{
    using Data;
    using Frames;
    using Interactions;
    using Layouts;
    using Logic;

    public class PointViewVerticalItemLogic : ViewItemLogic, IPointViewVerticalItemLogic 
    {
        public PointViewVerticalItemLogic(
            IViewItem            item,
            IViewItemFrameController       itemFrame,
            IViewItemInteraction itemInteraction,
            IViewItemDataModel   itemModel,
            IViewItemLayout      itemLayout)
            : base(item, itemFrame, itemInteraction, itemModel, itemLayout)
        {
        }

        public new IPointViewItemLayout ItemLayout => (IPointViewItemLayout)base.ItemLayout;
    }
}