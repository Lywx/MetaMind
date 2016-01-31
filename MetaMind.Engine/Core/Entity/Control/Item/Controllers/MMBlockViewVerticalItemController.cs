namespace MetaMind.Engine.Core.Entity.Control.Item.Controllers
{
    using Data;
    using Frames;
    using Interactions;
    using Layers;
    using Layouts;

    public class MMBlockViewVerticalItemController : MMPointViewVerticalItemController, IMMBlockViewVerticalItemController
    {
        public MMBlockViewVerticalItemController(
            IMMViewItem item,
            IMMViewItemFrameController itemFrame,
            IMMViewItemInteraction itemInteraction,
            IMMViewItemDataModel itemModel,
            IMMViewItemLayout itemLayout)
            : base(item, itemFrame, itemInteraction, itemModel, itemLayout)
        {
        }

        public new IMMBlockViewVerticalItemLayout ItemLayout => (IMMBlockViewVerticalItemLayout)base.ItemLayout;
    }
}