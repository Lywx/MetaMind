namespace MetaMind.Perseverance.Guis.Widgets.Motivations.Items
{
    using MetaMind.Engine.Guis.Elements.ViewItems;

    using Microsoft.Xna.Framework;

    public class MotivationItemTaskGraphics : ViewItemComponent
    {
        public MotivationItemTaskGraphics(IViewItem item)
            : base(item)
        {
        }

        public void Draw(GameTime gameTime, byte alpha)
        {
            if (ItemControl.Tracer != null)
            {
                ItemControl.Tracer.Draw(gameTime, alpha);
            }
        }
    }
}