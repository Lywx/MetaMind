namespace MetaMind.Engine.Guis.Widgets.Items.Interactions
{
    using Data;
    using Items;
    using Items.Layers;
    using Items.Layouts;
    using Services;
    using Views.Layers;
    using Views.Scrolls;
    using Views.Swaps;

    public class BlockViewVerticalItemInteraction : PointViewItemInteraction
    {
        private IBlockViewVerticalItemLayout itemLayout;

        private IViewSwapController viewSwap;

        private IViewScrollController viewScroll;

        private IViewBinding viewBinding;

        public BlockViewVerticalItemInteraction(
            IViewItem item,
            IViewItemLayout itemLayout,
            IViewItemLayoutInteraction itemLayoutInteraction)
            : base(item, itemLayout, itemLayoutInteraction)
        {
        }

        public override void SetupLayer()
        {
            base.SetupLayer();

            var itemLayer = this.ItemGetLayer<BlockViewVerticalItemLayer>();
            this.itemLayout = itemLayer.ItemLayout;

            var viewLayer = this.ViewGetLayer<ViewLayer>();
            this.viewSwap = viewLayer.ViewSwap;
            this.viewScroll = viewLayer.ViewScroll;
            this.viewBinding = viewLayer.ViewBinding;
        }

        public override void ViewDoSwap(IGameInteropService interop, IViewItem draggingItem)
        {
            if (this.Item[ItemState.Item_Is_Swaping]())
            {
                return;
            }

            var draggingItemLayer  = draggingItem.GetLayer<BlockViewVerticalItemLayer>();
            var draggingItemLayout = draggingItemLayer.ItemLayout;

            var draggingViewLayer  = draggingItem.View.GetLayer<ViewLayer>();
            var draggingViewScroll = draggingViewLayer.ViewScroll;

            this.viewSwap.StartProcess(interop,
                this.Item,
                this.viewScroll.Position(this.itemLayout.Row),
                draggingItem,
                draggingItem.View,
                draggingViewScroll.Position(draggingItemLayout.Row));
        }
    }
}