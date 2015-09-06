namespace MetaMind.Engine.Components
{
    using Fonts;
    using Graphics;
    using Microsoft.Xna.Framework.Graphics;

    internal class GameNullGraphics : IGameGraphics
    {
        public GraphicsManager Manager { get; }

        public IScreenManager Screen { get; }

        public GraphicsSettings Settings { get; }

        public SpriteBatch SpriteBatch { get; }

        public IStringDrawer StringDrawer { get; }

        public GraphicsDevice GraphicsDevice { get; }

        public void Initialize()
        {
        }

        public void Dispose()
        {
        }
    }
}