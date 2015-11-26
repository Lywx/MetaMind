namespace MetaMind.Engine.Entities.Controls.Item.Layers
{
    using Controllers;
    using Data;
    using Frames;
    using Interactions;
    using Layouts;

    public class MMPointViewVerticalItemController : MMViewItemController, IMMPointViewVerticalItemController 
    {
        public MMPointViewVerticalItemController(
            IMMViewItem            item,
            IMMViewItemFrameController       itemFrame,
            IMMViewItemInteraction itemInteraction,
            IMMViewItemDataModel   itemModel,
            IMMViewItemLayout      itemLayout)
            : base(item, itemFrame, itemInteraction, itemModel, itemLayout)
        {
        }

        public new IMMPointViewItemLayout ItemLayout => (IMMPointViewItemLayout)base.ItemLayout;
    }
}