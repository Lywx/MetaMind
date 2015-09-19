namespace MetaMind.Engine.Service
{
    using Component.Font;
    using Component.Graphics;
    using Microsoft.Xna.Framework.Graphics;

    public interface IGameGraphicsService
    {
        GraphicsManager Manager { get; }

        GraphicsSettings Settings { get; }

        SpriteBatch SpriteBatch { get; }
        
        IStringDrawer StringDrawer { get; }

        GraphicsDevice GraphicsDevice { get; }
    }
}