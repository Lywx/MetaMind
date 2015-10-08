namespace MetaMind.Engine.Components
{
    using System;
    using Microsoft.Xna.Framework;
    using Services;

    public abstract class MMMvcComponent<TMvcSettings, TMvcLogic, TMvcVisual> : MMInputableComponent, IMMMvcComponent<TMvcSettings, TMvcLogic, TMvcVisual>
        where                            TMvcLogic                            : IMMMvcComponentLogic<TMvcSettings>
        where                            TMvcVisual                           : IMMMvcComponentVisual<TMvcSettings>
    {
        #region Constructors

        protected MMMvcComponent(TMvcSettings settings, MMEngine engine)
            : base(engine)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            if (engine == null)
            {
                throw new ArgumentNullException(nameof(engine));
            }

            this.Settings = settings;
            this.Engine   = engine;
        }

        ~MMMvcComponent()
        {
            this.Dispose(true);
        }

        #endregion

        public TMvcSettings Settings { get; protected set; }

        public TMvcLogic Logic { get; protected set; }

        public TMvcVisual Visual { get; protected set; }

        #region Draw

        public override void BeginDraw(GameTime time)
        {
            base        .BeginDraw(time);
            this.Visual?.BeginDraw(time);
        }

        public override void Draw(GameTime time)
        {
            base        .Draw(time);
            this.Visual?.Draw(time);
        }

        public override void EndDraw(GameTime time)
        {
            base        .EndDraw(time);
            this.Visual?.EndDraw(time);
        }

        #endregion

        #region Update

        public void UpdateInput(IMMEngineInputService input, GameTime time)
        {
            this.Logic ?.Update(time);
            this.Visual?.Update(time);
        }

        public override void Update(GameTime time)
        {
            this.Logic ?.Update(time);
            this.Visual?.Update(time);
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
                        this.Logic?.Dispose();
                        this.Logic = default(TMvcLogic);

                        this.Visual?.Dispose();
                        this.Visual = default(TMvcVisual);
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