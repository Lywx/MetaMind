namespace MetaMind.Engine.Guis.Widgets.Items.Interactions
{
    using Items;
    using Items.Layers;
    using Items.Layouts;
    using Services;

    public class BlockViewVerticalItemInteraction : PointViewItemInteraction
    {
        private IBlockViewVerticalItemLayout itemLayout;

        public BlockViewVerticalItemInteraction(
            IViewItem                  item,
            IViewItemLayout            itemLayout,
            IViewItemLayoutInteraction itemLayoutInteraction)
            : base(item, itemLayout, itemLayoutInteraction)
        {
        }

        public override void SetupLayer()
        {
            base.SetupLayer();

            var itemLayer = this.ItemGetLayer<BlockViewVerticalItemLayer>();
            this.itemLayout = itemLayer.ItemLayout;
        }

        public override void ViewDoSwap(IGameInteropService interop, IViewItem draggingItem)
        {
            if (this.Item[ItemState.Item_Is_Swaping]())
            {
                return;
            }

            var draggingItemLayer  = draggingItem.GetLayer<BlockViewVerticalItemLayer>();
            var draggingItemLayout = draggingItemLayer.ItemLayout;
            var draggingViewScroll = draggingItem.View.ViewLogic.ViewScroll;

            this.View.ViewLogic.ViewSwap.StartProcess(interop,
                this.Item,
                this.View.ViewLogic.ViewScroll.Position(this.itemLayout.Row),
                draggingItem,
                draggingItem.View,
                draggingViewScroll.Position(draggingItemLayout.Row),
                this.View.ViewData);
        }
    }
}