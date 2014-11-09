using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Guis.Widgets.Items
{
    public interface IItemGraphics
    {
        void Draw( GameTime gameTime, byte alpha );

        void Update( GameTime gameTime );
    }
}