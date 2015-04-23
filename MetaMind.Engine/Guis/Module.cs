namespace MetaMind.Engine.Guis
{
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    /// FIXME: Won't work now.
    /// <summary>
    /// Module is the most outer shell of gui object that load and unload
    /// data from data source. The behavior is of maximum abstraction.
    /// </summary>
    /// <remarks>
    /// Compatible with previous GameControllableEntity implementation, as long as
    /// the derived class override the widget implementation.
    /// </remarks>
    public class Module<TModuleSettings> : GameControllableEntity, IModule
    {
        #region Constructors

        protected Module(TModuleSettings settings)
        {
            this.Settings = settings;
        }

        #endregion

        #region Components

        public IModuleControl Control { get; protected set; }

        public IModuleGraphics Graphics { get; protected set; }

        public TModuleSettings Settings { get; protected set; }

        #endregion

        #region Draw

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            if (this.Graphics != null)
            {
                this.Graphics.Draw(graphics, time, alpha);
            }
        }

        #endregion
        
        #region Load and Unload

        public override void LoadContent(IGameInteropService interop)
        {
            if (this.Control != null)
            {
                this.Control.LoadContent(interop);
            }

            base.LoadContent(interop);
        }

        public override void UnloadContent(IGameInteropService interop)
        {
            if (this.Control != null)
            {
                this.Control.UnloadContent(interop);
            }

            base.UnloadContent(interop);
        }

        #endregion
        
        #region Update

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            if (this.Control != null)
            {
                this.Control.UpdateInput(input, time);
            }

            if (this.Graphics != null)
            {
                this.Graphics.UpdateInput(input, time);
            }
        }

        public override void Update(GameTime time)
        {
            if (this.Control != null)
            {
                this.Control.Update(time);
            }

            if (this.Graphics != null)
            {
                this.Graphics.Update(time);
            }
        }

        #endregion
    }
}