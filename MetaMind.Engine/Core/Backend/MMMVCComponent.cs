namespace MetaMind.Engine.Core.Backend
{
    using System;
    using Microsoft.Xna.Framework;

    public abstract class MMMVCComponent<TMVCSettings, TMVCController, TMVCRenderer> : ImmInputableComponent, IMMMVCComponent<TMVCSettings, TMVCController, TMVCRenderer>
        where                            TMVCController                              : IMMMVCComponentController<TMVCSettings>
        where                            TMVCRenderer                                : IMMMVCComponentRenderer<TMVCSettings>
    {
        #region Constructors

        protected MMMVCComponent(TMVCSettings settings, MMEngine engine)
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

        ~MMMVCComponent()
        {
            this.Dispose(true);
        }

        #endregion

        public TMVCSettings Settings { get; protected set; }

        public TMVCController Controller { get; protected set; }

        public TMVCRenderer Renderer { get; protected set; }

        #region Draw

        public override void BeginDraw(GameTime time)
        {
            base          .BeginDraw(time);
            this.Renderer?.BeginDraw(time);
        }

        public override void Draw(GameTime time)
        {
            base          .Draw(time);
            this.Renderer?.Draw(time);
        }

        public override void EndDraw(GameTime time)
        {
            base          .EndDraw(time);
            this.Renderer?.EndDraw(time);
        }

        #endregion

        #region Update

        public override void UpdateInput(GameTime time)
        {
            base.UpdateInput(time);

            this.Controller?.Update(time);
            this.Renderer?  .Update(time);
        }

        public override void Update(GameTime time)
        {
            base.Update(time);

            this.Controller?.Update(time);
            this.Renderer?  .Update(time);
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
                        this.Controller?.Dispose();
                        this.Controller = default(TMVCController);

                        this.Renderer?.Dispose();
                        this.Renderer = default(TMVCRenderer);
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