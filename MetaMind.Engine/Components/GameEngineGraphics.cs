namespace MetaMind.Engine.Components
{
    using MetaMind.Engine.Components.Fonts;
    using MetaMind.Engine.Components.Graphics;

    using Microsoft.Xna.Framework.Graphics;

    public class GameEngineGraphics : IGameGraphics
    {
        public GraphicsManager Manager { get; private set; }
        
        public GraphicsSettings Settings { get; private set; }

        public SpriteBatch SpriteBatch { get; private set; }

        public IStringDrawer String { get; private set; }

        public GameEngineGraphics(GameEngine engine)
        {
            this.SpriteBatch = new SpriteBatch(engine.GraphicsDevice);
            this.String      = new StringDrawer(this.SpriteBatch);
            
            this.Settings = new GraphicsSettings();
            this.Manager  = new GraphicsManager(engine, this.Settings);
        }

        public void Initialize()
        {
            this.Manager.Initialize();
        }
    }
}