namespace MetaMind.Engine.Gui.Controls.Item.Frames
{
    using System;
    using Layers;
    using Microsoft.Xna.Framework;
    using Views.Layers;

    public class PointViewItemFrameController : ViewItemFrameController
    {
        public PointViewItemFrameController(IViewItem item, ViewItemImmRectangle itemImmRootRectangle)
            : base(item, itemImmRootRectangle)
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
                if (!this.Item[ViewItemState.Item_Is_Dragging]() &&
                    !this.Item[ViewItemState.Item_Is_Swaping]())
                {
                    return viewScroll.Position(itemLayout.Id);
                }

                if (this.Item[ViewItemState.Item_Is_Swaping]())
                {
                    return viewSwap.Position;
                }

                return this.RootImmRectangle.Location.ToVector2();
            };
        }

        public virtual Func<Vector2> RootFrameLocation { get; set; }

        protected override void UpdateFrameGeometry()
        {
            this.RootImmRectangle.Location = this.RootFrameLocation().ToPoint();
        }
    }
}