namespace MetaMind.Engine.Core.Entity.Control.Item.Visuals
{
    using Entity.Input;
    using Images;
    using Microsoft.Xna.Framework;

    public class MMViewItemRectangleRender : MMViewItemControllerComponent, IMMViewItemRendererComponent
    {
        public MMViewItemRectangleRender(IMMViewItem item, IMMPressableRectangleElement rectangle, MMViewItemRenderSettings settings)
            : base(item)
        {
            this.Rectangle = rectangle;
            this.Settings  = settings;

            this.Filling = new ImageBox(
                this.Item.GetImageSelector(this.Settings),
                this.Item.GetBoundsSelector(this.Settings, this.Rectangle.Bounds),
                this.Item.GetColorSelector(this.Settings));

            this.Boundary = new ColorBox(
                this.Item.GetBoundsSelector(this.Settings, this.Rectangle.Bounds),
                this.Item.GetColorSelector(this.Settings))
            {
                ColorFilled = false
            };
        }

        #region Dependency

        public MMViewItemRenderSettings Settings { get; }

        #endregion

        public ColorBox Boundary { get; }

        public ImageBox Filling { get; }

        public IMMPressableRectangleElement Rectangle { get; }

        public override void Draw(GameTime time)
        {
            if (!MMViewItemState.Item_Is_Active.Match(this.Item))
            {
                return;
            }

            this.Boundary.Draw(time);
            this.Filling .Draw(time);
        }
    }
}