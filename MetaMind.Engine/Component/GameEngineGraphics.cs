namespace MetaMind.Engine.Component
{
    using System;
    using Content.Font;
    using Graphics;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class GameEngineGraphics : DrawableGameComponent, IGameGraphics
    {
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

            // No dependency injection here, because font manager is a class focus on font 
            // loading. It may extend but in the form of more internal operations.
            // The functionality is never extended in the form of inheritance. 
            this.FontManager = new FontManager(engine);
            this.Game.Components.Add(this.FontManager);

            // No dependency injection here, because string drawer is a class focus on string 
            // drawing. The functionality is never extended in the form of inheritance.
            this.Renderer = new Renderer(this.SpriteBatch, this.FontManager);
            this.Game.Components.Add(this.Renderer);
        }

        public GraphicsManager Manager { get; private set; }
        
        public GraphicsSettings Settings { get; private set; }

        public SpriteBatch SpriteBatch { get; private set; }

        public IRenderer Renderer { get; private set; }

        public IFontManager FontManager { get; private set; }

        #region Initialization

        public override void Initialize()
        {
            this.Manager.Initialize();
        }

        #endregion

        #region IDisposable

        private bool IsDisposed { get; set; }

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    if (!this.IsDisposed)
                    {
                        this.Manager?.Dispose();
                        this.Manager = null;

                        this.SpriteBatch?.Dispose();
                        this.SpriteBatch = null;

                        this.FontManager?.Dispose();
                        this.FontManager = null;
                    }

                    this.IsDisposed = true;
                }
            }
            catch
            {
                // Ignored
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        #endregion
    }
}