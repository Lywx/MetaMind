namespace MetaMind.Engine.Services
{
    using Components.Graphics;
    using Microsoft.Xna.Framework.Graphics;

    public interface IMMEngineGraphicsService
    {
        MMGraphicsManager Manager { get; }

        MMGraphicsSettings Settings { get; }

        SpriteBatch SpriteBatch { get; }
        
        IMMRenderer Renderer { get; }

        GraphicsDevice GraphicsDevice { get; }
    }
}