
namespace MetaMind.Engine.Guis.Controls.Items.Logic
{
    using Data;
    using Frames;
    using Interactions;
    using Layouts;

    public class PointView2DItemLogic : ViewItemLogic, IPointView2DItemLogic 
    {
        #region Constructors

        public PointView2DItemLogic(IViewItem item, IViewItemFrame itemFrame, IViewItemInteraction itemInteraction, IViewItemDataModel itemModel, IViewItemLayout itemLayout)
            : base(item, itemFrame, itemInteraction, itemModel, itemLayout)
        {
        }

        #endregion Constructors

        public new IPointViewItemLayout ItemLayout
        {
            get { return (IPointViewItemLayout)base.ItemLayout; }
        }
    }
}