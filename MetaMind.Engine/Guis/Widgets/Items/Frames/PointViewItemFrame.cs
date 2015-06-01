namespace MetaMind.Engine.Guis.Widgets.Items.Frames
{
    using System;

    using MetaMind.Engine.Guis.Widgets.Items.Layers;
    using MetaMind.Engine.Guis.Widgets.Items.Layouts;
    using MetaMind.Engine.Guis.Widgets.Views.Scrolls;
    using MetaMind.Engine.Guis.Widgets.Views.Swaps;

    public class PointViewItemFrame : ViewItemFrame
    {
        private readonly IViewScrollController viewScroll;

        private readonly IViewSwapController viewSwap;

        private readonly IViewItemLayout itemLayout;

        public PointViewItemFrame(IViewItem item, IViewItemLayout itemLayout, IViewScrollController viewScroll, IViewSwapController viewSwap)
            : base(item)
        {
            if (itemLayout == null)
            {
                throw new ArgumentNullException("itemLayout");
            }

            this.itemLayout = itemLayout;

            if (viewScroll == null)
            {
                throw new ArgumentNullException("viewScroll");
            }

            if (viewSwap == null)
            {
                throw new ArgumentNullException("viewSwap");
            }

            this.viewScroll = viewScroll;
            this.viewSwap   = viewSwap;
        }

        protected override void UpdateFrameGeometry()
        {
            if (!this.Item[ItemState.Item_Is_Dragging]() && 
                !this.Item[ItemState.Item_Is_Swaping]())
            {
                this.RootFrame.Center = this.viewScroll.Position(this.itemLayout.Id).ToPoint();
            }
            else if (this.Item[ItemState.Item_Is_Swaping]())
            {
                this.RootFrame.Center = this.viewSwap.Position.ToPoint();
            }
        }
    }
}