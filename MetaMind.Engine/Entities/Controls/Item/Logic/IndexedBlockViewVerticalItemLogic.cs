namespace MetaMind.Engine.Entities.Controls.Item.Logic
{
    using Data;
    using Frames;
    using Interactions;
    using Layouts;

    public class MMIndexedBlockViewVerticalItemController : MMIndexBlockViewVerticalItemLogic
    {
        public MMIndexedBlockViewVerticalItemController(
            IMMViewItem item,
            IMMViewItemFrameController itemFrame,
            IMMViewItemInteraction itemInteraction,
            IMMViewItemDataModel itemModel,
            IMMViewItemLayout itemLayout)
            : base(item, itemFrame, itemInteraction, itemModel, itemLayout)
        {
        }
    }
}