namespace MetaMind.Engine.Core.Backend
{
    using System;
    using Graphics;
    using Microsoft.Xna.Framework;
    using Services;

    public abstract class MMGeneralComponent : DrawableGameComponent, IMMGeneralComponent
    {
        #region Constructors

        protected MMGeneralComponent(MMEngine engine)
            : base(engine)
        {
            if (engine == null)
            {
                throw new ArgumentNullException(nameof(engine));
            }

            this.Engine = engine;
        }

        #endregion

        #region Update

        public virtual void UpdateInput(GameTime time)
        {

        }

        #endregion

        #region Service Data

        public MMEngine Engine { get; protected set; }

        protected IMMEngineInputService GlobalInput => MMEngine.Service.Input;

        protected IMMEngineInteropService GlobalInterop
            => MMEngine.Service.Interop;

        protected IMMEngineGraphicsService GlobalGraphics
            => MMEngine.Service.Graphics;

        protected IMMRenderer GlobalGraphicsRenderer
            => this.GlobalGraphics.Renderer;

        protected IMMEngineNumericalService GlobalNumerical
            => MMEngine.Service.Numerical;

        #endregion

        #region Draw

        public virtual void BeginDraw(GameTime time)
        {

        }

        public virtual void EndDraw(GameTime time)
        {

        }

        #endregion
    }
}