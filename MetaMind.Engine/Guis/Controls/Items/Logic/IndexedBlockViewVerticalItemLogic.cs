namespace MetaMind.Engine.Guis.Widgets.Items.Logic
{
    using Data;
    using Frames;
    using Interactions;
    using Layouts;

    public class IndexedBlockViewVerticalItemLogic : IndexBlockViewVerticalItemLogic
    {
        public IndexedBlockViewVerticalItemLogic(
            IViewItem item,
            IViewItemFrame itemFrame,
            IViewItemInteraction itemInteraction,
            IViewItemDataModel itemModel,
            IViewItemLayout itemLayout)
            : base(item, itemFrame, itemInteraction, itemModel, itemLayout)
        {
        }
    }
}