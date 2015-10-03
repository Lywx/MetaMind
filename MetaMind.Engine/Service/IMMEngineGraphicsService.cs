namespace MetaMind.Engine.Service
{
    using Components.Graphics;
    using Microsoft.Xna.Framework.Graphics;

    public interface IMMEngineGraphicsService
    {
        MMGraphicsManager Manager { get; }

        MMGraphicsSettings Settings { get; }

        SpriteBatch SpriteBatch { get; }
        
        IMMRenderer MMRenderer { get; }

        GraphicsDevice GraphicsDevice { get; }
    }
}