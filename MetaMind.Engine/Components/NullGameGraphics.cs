namespace MetaMind.Engine.Components
{
    using MetaMind.Engine.Components.Fonts;
    using MetaMind.Engine.Components.Graphics;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    internal class NullGameGraphics : IGameGraphics
    {
        public GraphicsManager Manager { get; private set; }

        public IScreenManager Screen { get; private set; }

        public GraphicsSettings Settings { get; private set; }

        public SpriteBatch SpriteBatch { get; private set; }

        public IStringDrawer String { get; private set; }

        public void Initialize()
        {
            
        }

        public void UpdateInput(GameTime gameTime)
        {
            
        }
    }
}