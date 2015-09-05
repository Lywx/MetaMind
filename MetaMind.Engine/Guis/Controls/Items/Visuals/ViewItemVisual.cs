namespace MetaMind.Engine.Guis.Controls.Items.Visuals
{
    using Microsoft.Xna.Framework;
    using Services;

    public abstract class ViewItemVisual : ViewItemComponent, IViewItemVisual
    {
        public ViewItemVisual(IViewItem item)
            : base(item)
        {
        }

        public abstract override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha);
    }
}