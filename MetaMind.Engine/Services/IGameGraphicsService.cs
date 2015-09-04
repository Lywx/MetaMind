namespace MetaMind.Engine.Services
{
    using Components.Fonts;
    using Components.Graphics;
    using Microsoft.Xna.Framework.Graphics;

    public interface IGameGraphicsService
    {
        GraphicsManager Manager { get; }

        GraphicsSettings Settings { get; }

        SpriteBatch SpriteBatch { get; }
        
        IStringDrawer StringDrawer { get; }
    }
}