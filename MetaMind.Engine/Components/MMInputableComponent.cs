namespace MetaMind.Engine.Components
{
    using System;
    using Graphics;
    using Microsoft.Xna.Framework;
    using Services;

    public abstract class MMInputableComponent : DrawableGameComponent, IMMInputableComponent
    {
        #region Constructors

        protected MMInputableComponent(MMEngine engine)
            : base(engine)
        {
            if (engine == null)
            {
                throw new ArgumentNullException(nameof(engine));
            }

            this.Engine = engine;
        }

        #endregion

        #region Service Data

        public MMEngine Engine { get; protected set; }

        protected IMMEngineInputService Input => MMEngine.Service.Input;

        protected IMMEngineInteropService Interop => MMEngine.Service.Interop;

        protected IMMEngineGraphicsService Graphics => MMEngine.Service.Graphics;

        protected IMMRenderer GraphicsRenderer => this.Graphics.Renderer;

        protected IMMEngineNumericalService Numerical => MMEngine.Service.Numerical;

        #endregion

        #region Draw

        public virtual void BeginDraw(GameTime time)
        {
        }

        public virtual void EndDraw(GameTime time)
        {
        }

        #endregion

        #region Update

        public virtual void UpdateInput(GameTime time)
        {
        }

        #endregion
    }
}