namespace MetaMind.Engine.Core.Entity.Control.Item.Frames
{
    using Layers;
    using Views.Layers;

    public class MMBlockViewVerticalItemFrameController : MMPointViewItemFrameController
    {
        public MMBlockViewVerticalItemFrameController(
            IMMViewItem item,
            ViewItemImmRectangle itemImmRootRectangle) 
            : base(item, itemImmRootRectangle)
        {
        }

        public override void Initialize()
        {
            base.Initialize();

            var itemLayer = this.GetItemLayer<MMBlockViewVerticalItemLayer>();
            var itemLayout = itemLayer.ItemLogic.ItemLayout;

            var viewLayer = this.GetViewLayer<MMPointViewLayer>();
            var viewScroll = viewLayer.ViewScroll;
            var viewSwap = viewLayer.ViewSwap;

            this.RootFrameLocation = () =>
            {
                if (!this.Item[MMViewItemState.Item_Is_Dragging]() &&
                    !this.Item[MMViewItemState.Item_Is_Swaping]())
                {
                    return viewScroll.Position(itemLayout.Row);
                }

                if (this.Item[MMViewItemState.Item_Is_Swaping]())
                {
                    return viewSwap.Position;
                }

                return this.RootImmRectangle.Location.ToVector2();
            };
        }
    }
}