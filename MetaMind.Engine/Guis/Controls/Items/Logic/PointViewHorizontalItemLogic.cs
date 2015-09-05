namespace MetaMind.Engine.Guis.Controls.Items.Logic
{
    using Data;
    using Frames;
    using Interactions;
    using Layouts;

    public class PointViewHorizontalItemLogic : ViewItemLogic, IPointViewHorizontalItemLogic
    {
        public PointViewHorizontalItemLogic(IViewItem item, IViewItemFrame itemFrame, IViewItemInteraction itemInteraction, IViewItemDataModel itemModel, IViewItemLayout itemLayout) : base(item, itemFrame, itemInteraction, itemModel, itemLayout)
        {
        }

        public new IPointViewItemLayout ItemLayout
        {
            get { return (IPointViewItemLayout)base.ItemLayout; }
        }
    }
}