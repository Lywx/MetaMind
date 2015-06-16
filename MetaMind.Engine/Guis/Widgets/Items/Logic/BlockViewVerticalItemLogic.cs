namespace MetaMind.Engine.Guis.Widgets.Items.Logic
{
    using Data;
    using Frames;
    using Interactions;
    using Layers;
    using Layouts;

    public class BlockViewVerticalItemLogic : PointViewVerticalItemLogic, IBlockViewVerticalItemLogic
    {
        public BlockViewVerticalItemLogic(
            IViewItem item,
            IViewItemFrame itemFrame,
            IViewItemInteraction itemInteraction,
            IViewItemDataModel itemModel,
            IViewItemLayout itemLayout)
            : base(item, itemFrame, itemInteraction, itemModel, itemLayout)
        {
        }

        public new IBlockViewVerticalItemLayout ItemLayout
        {
            get
            {
                return (IBlockViewVerticalItemLayout)base.ItemLayout;
            } 
        }
    }
}