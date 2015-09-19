namespace MetaMind.Engine.Gui.Control.Item.Logic
{
    using Data;
    using Frames;
    using Interactions;
    using Layers;
    using Layouts;

    public class BlockViewVerticalItemLogic : PointViewVerticalItemLogic, IBlockViewVerticalItemLogic
    {
        public BlockViewVerticalItemLogic(
            IViewItem item,
            IViewItemFrameController itemFrame,
            IViewItemInteraction itemInteraction,
            IViewItemDataModel itemModel,
            IViewItemLayout itemLayout)
            : base(item, itemFrame, itemInteraction, itemModel, itemLayout)
        {
        }

        public new IBlockViewVerticalItemLayout ItemLayout => (IBlockViewVerticalItemLayout)base.ItemLayout;
    }
}