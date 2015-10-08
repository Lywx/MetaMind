namespace MetaMind.Engine.Components
{
    using System;
    using Graphics;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class MMEngineGraphics : DrawableGameComponent, IMMEngineGraphics
    {
        public MMEngineGraphics(MMEngine engine, MMGraphicsSettings settings)
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

            this.Settings = settings;
            this.Manager  = new MMGraphicsManager(engine, settings);

            // No dependency injection here, because sprite batch is never replaced as long 
            // as this is a MonoGame application.
            this.SpriteBatch = new SpriteBatch(this.Manager.GraphicsDevice);

            // No dependency injection here, because string drawer is a class focus on string 
            // drawing. The functionality is never extended in the form of inheritance.
            this.Renderer = new MMRenderer(this.SpriteBatch);
            this.Game.Components.Add(this.Renderer);
        }

        public MMGraphicsManager Manager { get; private set; }
        
        public MMGraphicsSettings Settings { get; private set; }

        public SpriteBatch SpriteBatch { get; private set; }

        public IMMRenderer Renderer { get; private set; }

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