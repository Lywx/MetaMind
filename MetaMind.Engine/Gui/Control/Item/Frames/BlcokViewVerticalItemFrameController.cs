namespace MetaMind.Engine.Gui.Control.Item.Frames
{
    using Layers;
    using Views.Layers;

    public class BlcokViewVerticalItemFrameController : PointViewItemFrameController
    {
        public BlcokViewVerticalItemFrameController(
            IViewItem item,
            ViewItemRectangle itemRootRectangle) 
            : base(item, itemRootRectangle)
        {
        }

        public override void Initialize()
        {
            base.Initialize();

            var itemLayer = this.GetItemLayer<BlockViewVerticalItemLayer>();
            var itemLayout = itemLayer.ItemLogic.ItemLayout;

            var viewLayer = this.GetViewLayer<PointViewLayer>();
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

                return this.RootRectangle.Location.ToVector2();
            };
        }
    }
}