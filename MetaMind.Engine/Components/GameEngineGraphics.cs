namespace MetaMind.Engine.Components
{
    using MetaMind.Engine.Components.Fonts;
    using MetaMind.Engine.Components.Graphics;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class GameEngineGraphics : DrawableGameComponent, IGameGraphics
    {
        public GraphicsManager Manager { get; private set; }
        
        public GraphicsSettings Settings { get; private set; }

        public SpriteBatch SpriteBatch { get; private set; }

        public IStringDrawer StringDrawer { get; private set; }

        public IFontManager Font { get; private set; }

        public GameEngineGraphics(GameEngine engine)
            : base(engine)
        {
            this.Settings = new GraphicsSettings();
            this.Manager  = new GraphicsManager(engine, this.Settings);
            
            this.SpriteBatch  = new SpriteBatch(this.Manager.GraphicsDevice);
            this.StringDrawer = new StringDrawer(this.SpriteBatch);
            
            this.Font = new FontManager(engine);
        }

        public override void Initialize()
        {
            this.Manager.Initialize();
        }

        protected override void LoadContent()
        {
        }
    }
}