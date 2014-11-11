namespace MetaMind.Engine.Guis.Elements.Items
{
    using Microsoft.Xna.Framework;

    public interface IItemGraphics
    {
        void Draw( GameTime gameTime, byte alpha );

        void Update( GameTime gameTime );
    }
}