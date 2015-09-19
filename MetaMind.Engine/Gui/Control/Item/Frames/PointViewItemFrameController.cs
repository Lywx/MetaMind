namespace MetaMind.Engine.Gui.Control.Item.Frames
{
    using System;
    using Layers;
    using Microsoft.Xna.Framework;
    using Views.Layers;

    public class PointViewItemFrameController : ViewItemFrameController
    {
        public PointViewItemFrameController(IViewItem item, ViewItemRectangle itemRootRectangle)
            : base(item, itemRootRectangle)
        {
        }

        public override void Initialize()
        {
            base.Initialize();

            var itemLayer = this.GetItemLayer<PointViewItemLayer>();
            var itemLayout = itemLayer.ItemLogic.ItemLayout;

            var viewLayer = this.GetViewLayer<PointViewLayer>();
            var viewScroll = viewLayer.ViewScroll;
            var viewSwap = viewLayer.ViewSwap;

            this.RootFrameLocation = () =>
            {
                if (!this.Item[ItemState.Item_Is_Dragging]() &&
                    !this.Item[ItemState.Item_Is_Swaping]())
                {
                    return viewScroll.Position(itemLayout.Id);
                }

                if (this.Item[ItemState.Item_Is_Swaping]())
                {
                    return viewSwap.Position;
                }

                return this.RootRectangle.Location.ToVector2();
            };
        }

        public virtual Func<Vector2> RootFrameLocation { get; set; }

        protected override void UpdateFrameGeometry()
        {
            this.RootRectangle.Location = this.RootFrameLocation().ToPoint();
        }
    }
}