namespace MetaMind.Engine.Components
{
    using System;
    using Fonts;
    using Graphics;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class GameEngineGraphics : DrawableGameComponent, IGameGraphics
    {
        public GraphicsManager Manager { get; private set; }
        
        public GraphicsSettings Settings { get; private set; }

        public SpriteBatch SpriteBatch { get; private set; }

        public IStringDrawer StringDrawer { get; private set; }

        public IFontManager FontManager { get; private set; }

        public GameEngineGraphics(GameEngine engine, GraphicsSettings settings, GraphicsManager manager)
            : base(engine)
        {
            if (engine == null)
            {
                throw new ArgumentNullException(nameof(engine));
            }

            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }

            this.Settings = settings;
            this.Manager  = manager;
            
            // No dependency injection here, because sprite batch is never replaced as long 
            // as this is a MonoGame application.
            this.SpriteBatch = new SpriteBatch(this.Manager.GraphicsDevice);

            // No dependency injection here, because string drawer is a class focus on string 
            // drawing. The functionality is never extended in the form of inheritance.
            this.StringDrawer = new StringDrawer(this.SpriteBatch);

            // No dependency injection here, because font manager is a class focus on font 
            // loading. It may extend but in the form of more internal operations.
            // The functionality is never extended in the form of inheritance. 
            this.FontManager = new FontManager(engine);
        }

        public override void Initialize()
        {
            this.Manager.Initialize();
        }

        protected override void LoadContent()
        {
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    this.Manager?.Dispose();
                    this.Manager = null;

                    this.SpriteBatch?.Dispose();
                    this.SpriteBatch = null;

                    this.FontManager?.Dispose();
                    this.FontManager = null;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }
    }
}