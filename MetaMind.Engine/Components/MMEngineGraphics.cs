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
            this.RendererManager = new MMRendererManager(engine);
            this.Game.Components.Add(this.RendererManager);

            // No dependency injection here, because string drawer is a class focus on string 
            // drawing. The functionality is never extended in the form of inheritance.
            this.Renderer = new MMRenderer(this.RendererManager);
        }

        public GraphicsDevice Device => this.Manager.GraphicsDevice;

        #region Setting Data

        public MMGraphicsManager Manager { get; private set; }
        
        public MMGraphicsSettings Settings { get; private set; }

        #endregion

        #region Render Data

        public MMRendererManager RendererManager { get; private set; }

        public IMMRenderer Renderer { get; private set; }

        #endregion

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

                        this.RendererManager?.Dispose();
                        this.RendererManager = null;
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