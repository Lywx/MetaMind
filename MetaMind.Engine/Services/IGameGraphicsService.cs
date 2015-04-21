namespace MetaMind.Engine.Services
{
    using MetaMind.Engine.Components;
    using MetaMind.Engine.Components.Fonts;
    using MetaMind.Engine.Components.Graphics;

    using Microsoft.Xna.Framework.Graphics;

    public interface IGameGraphicsService
    {
        GraphicsManager Manager { get; }

        GraphicsSettings Settings { get; }

        SpriteBatch SpriteBatch { get; }

        IScreenManager Screen { get; }
        
        IStringDrawer String { get; }
    }
}