
namespace MetaMind.Engine.Guis.Widgets.Items.Logic
{
    using MetaMind.Engine.Guis.Widgets.Items.Data;
    using MetaMind.Engine.Guis.Widgets.Items.Frames;
    using MetaMind.Engine.Guis.Widgets.Items.Interactions;

    public class PointView2DItemLogic : ViewItemLogic, IViewItemLogic
    {
        #region Constructors

        public PointView2DItemLogic(IViewItem item, IViewItemFrame itemFrame, IViewItemInteraction itemInteraction, IViewItemDataModel itemModel)
            : base(item, itemFrame, itemInteraction, itemModel)
        {
        }

        #endregion Constructors
    }
}