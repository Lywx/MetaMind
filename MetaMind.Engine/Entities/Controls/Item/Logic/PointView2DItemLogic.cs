
namespace MetaMind.Engine.Entities.Controls.Item.Logic
{
    using Data;
    using Frames;
    using Interactions;
    using Layouts;

    public class MMPointView2DItemController : MMViewItemController, IMMPointView2DItemController 
    {
        #region Constructors

        public MMPointView2DItemController(IMMViewItem item, IMMViewItemFrameController itemFrame, IMMViewItemInteraction itemInteraction, IMMViewItemDataModel itemModel, IMMViewItemLayout itemLayout)
            : base(item, itemFrame, itemInteraction, itemModel, itemLayout)
        {
        }

        #endregion Constructors

        public new IMMPointViewItemLayout ItemLayout
        {
            get { return (IMMPointViewItemLayout)base.ItemLayout; }
        }
    }
}