namespace MetaMind.Engine.Entities.Controls.Item.Interactions
{
    using Data;
    using Layers;
    using Layouts;
    using Services;
    using Views.Layers;
    using Views.Scrolls;
    using Views.Swaps;

    public class MMBlockViewVerticalItemInteraction : MMPointViewItemInteraction
    {
        private IMMBlockViewVerticalItemLayout itemLayout;

        private IMMViewSwapController viewSwap;

        private IMMViewScrollController viewScroll;

        private IViewBinding viewBinding;

        public MMBlockViewVerticalItemInteraction(
            IMMViewItem item,
            IMMViewItemLayout itemLayout,
            IViewItemLayoutInteraction itemLayoutInteraction)
            : base(item, itemLayout, itemLayoutInteraction)
        {
        }

        public override void Initialize()
        {
            base.Initialize();

            var itemLayer = this.GetItemLayer<MMBlockViewVerticalItemLayer>();
            this.itemLayout = itemLayer.ItemLayout;

            var viewLayer = this.GetViewLayer<ViewLayer>();
            this.viewSwap = viewLayer.ViewSwap;
            this.viewScroll = viewLayer.ViewScroll;
            this.viewBinding = viewLayer.ViewBinding;
        }

        public override void ViewDoSwap(IMMEngineInteropService interop, IMMViewItem draggingItem)
        {
            if (this.Item[MMViewItemState.Item_Is_Swaping]())
            {
                return;
            }

            var draggingItemLayer  = draggingItem.GetLayer<MMBlockViewVerticalItemLayer>();
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