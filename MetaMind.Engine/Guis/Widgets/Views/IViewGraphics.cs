using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Guis.Widgets.Views
{
    public interface IViewGraphics
    {
        void Update( GameTime gameTime );

        void Draw( GameTime gameTime );
    }
}