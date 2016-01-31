namespace MetaMind.Engine.Core.Entity.Control.Item.Frames
{
    using System;
    using Layers;
    using Microsoft.Xna.Framework;
    using Views.Layers;

    public class MMPointViewItemFrameController : MMViewItemFrameController
    {
        public MMPointViewItemFrameController(IMMViewItem item, ViewItemImmRectangle itemImmRootRectangle)
            : base(item, itemImmRootRectangle)
        {
        }

        public override void Initialize()
        {
            base.Initialize();

            var itemLayer = this.GetItemLayer<MMPointViewItemLayer>();
            var itemLayout = itemLayer.ItemLogic.ItemLayout;

            var viewLayer = this.GetViewLayer<MMPointViewLayer>();
            var viewScroll = viewLayer.ViewScroll;
            var viewSwap = viewLayer.ViewSwap;

            this.RootFrameLocation = () =>
            {
                if (!this.Item[MMViewItemState.Item_Is_Dragging]() &&
                    !this.Item[MMViewItemState.Item_Is_Swaping]())
                {
                    return viewScroll.Position(itemLayout.Id);
                }

                if (this.Item[MMViewItemState.Item_Is_Swaping]())
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