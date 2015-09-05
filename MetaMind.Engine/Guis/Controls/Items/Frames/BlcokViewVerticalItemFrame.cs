namespace MetaMind.Engine.Guis.Controls.Items.Frames
{
    using Layers;
    using Views.Layers;

    public class BlcokViewVerticalItemFrame : PointViewItemFrame
    {
        public BlcokViewVerticalItemFrame(
            IViewItem item,
            IViewItemRootFrame itemRootFrame) 
            : base(item, itemRootFrame)
        {
        }

        public override void SetupLayer()
        {
            base.SetupLayer();

            var itemLayer = this.ItemGetLayer<BlockViewVerticalItemLayer>();
            var itemLayout = itemLayer.ItemLogic.ItemLayout;

            var viewLayer = this.ViewGetLayer<PointViewLayer>();
            var viewScroll = viewLayer.ViewScroll;
            var viewSwap = viewLayer.ViewSwap;

            this.RootFrameLocation = () =>
            {
                if (!this.Item[ItemState.Item_Is_Dragging]() &&
                    !this.Item[ItemState.Item_Is_Swaping]())
                {
                    return viewScroll.Position(itemLayout.Row);
                }

                if (this.Item[ItemState.Item_Is_Swaping]())
                {
                    return viewSwap.Position;
                }

                return this.RootFrame.Location.ToVector2();
            };
        }
    }
}