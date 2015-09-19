namespace MetaMind.Engine.Gui.Control.Item.Visuals
{
    using Microsoft.Xna.Framework;
    using Service;

    public abstract class ViewItemVisual : ViewItemComponent, IViewItemVisual
    {
        public ViewItemVisual(IViewItem item)
            : base(item)
        {
        }

        /// <remarks>
        /// Forced reimplementation.
        /// </remarks>>
        public abstract override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha);
    }
}