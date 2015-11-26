namespace MetaMind.Engine.Entities.Controls.Item.Controllers
{
    using Data;
    using Frames;
    using Interactions;
    using Layouts;

    public class MMPointViewHorizontalItemController : MMViewItemController, IMMPointViewHorizontalItemController
    {
        public MMPointViewHorizontalItemController(IMMViewItem item, IMMViewItemFrameController itemFrame, IMMViewItemInteraction itemInteraction, IMMViewItemDataModel itemModel, IMMViewItemLayout itemLayout) : base(item, itemFrame, itemInteraction, itemModel, itemLayout)
        {
        }

        public new IMMPointViewItemLayout ItemLayout
        {
            get { return (IMMPointViewItemLayout)base.ItemLayout; }
        }
    }
}