namespace MetaMind.Engine.Guis.Widgets.Items.Frames
{
    using System;
    using Layers;

    using Microsoft.Xna.Framework;
    using Views.Layers;

    public class PointViewItemFrame : ViewItemFrame
    {
        public PointViewItemFrame(IViewItem item)
            : base(item)
        {
        }

        public override void SetupLayer()
        {
            base.SetupLayer();

            var itemLayer = this.ItemGetLayer<PointViewItemLayer>();
            var itemLayout = itemLayer.ItemLogic.ItemLayout;

            var viewLayer = this.ViewGetLayer<PointViewLayer>();
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

                return this.RootFrame.Location.ToVector2();
            };
        }

        public virtual Func<Vector2> RootFrameLocation { get; set; }

        protected override void UpdateFrameGeometry()
        {
            this.RootFrame.Location = this.RootFrameLocation().ToPoint();
        }
    }
}