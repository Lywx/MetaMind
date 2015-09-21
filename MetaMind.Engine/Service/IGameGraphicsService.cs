namespace MetaMind.Engine.Service
{
    using Component.Graphics;
    using Microsoft.Xna.Framework.Graphics;

    public interface IGameGraphicsService
    {
        GraphicsManager Manager { get; }

        GraphicsSettings Settings { get; }

        SpriteBatch SpriteBatch { get; }
        
        IRenderer Renderer { get; }

        GraphicsDevice GraphicsDevice { get; }
    }
}