namespace MetaMind.Engine.Gui.Control.Item.Logic
{
    using Data;
    using Frames;
    using Interactions;
    using Layouts;

    public class IndexedBlockViewVerticalItemLogic : IndexBlockViewVerticalItemLogic
    {
        public IndexedBlockViewVerticalItemLogic(
            IViewItem item,
            IViewItemFrameController itemFrame,
            IViewItemInteraction itemInteraction,
            IViewItemDataModel itemModel,
            IViewItemLayout itemLayout)
            : base(item, itemFrame, itemInteraction, itemModel, itemLayout)
        {
        }
    }
}