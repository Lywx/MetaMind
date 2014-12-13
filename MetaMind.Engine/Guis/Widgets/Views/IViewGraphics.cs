namespace MetaMind.Engine.Guis.Widgets.Views
{
    using Microsoft.Xna.Framework;

    public interface IViewGraphics
    {
        void UpdateStructure(GameTime gameTime);

        void Draw(GameTime gameTime, byte alpha);
    }
}