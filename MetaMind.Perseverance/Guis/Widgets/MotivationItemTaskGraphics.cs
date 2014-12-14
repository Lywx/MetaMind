namespace MetaMind.Perseverance.Guis.Widgets
{
    using MetaMind.Engine.Guis.Widgets.Items;

    using Microsoft.Xna.Framework;

    public class MotivationItemTaskGraphics : ViewItemComponent
    {
        public MotivationItemTaskGraphics(IViewItem item)
            : base(item)
        {
        }

        public void Draw(GameTime gameTime, byte alpha)
        {
            if (this.ItemControl.Tracer != null)
            {
                this.ItemControl.Tracer.Draw(gameTime, alpha);
            }
        }
    }
}