namespace MetaMind.Engine.Guis
{
    using MetaMind.Engine.Services;
    using Microsoft.Xna.Framework;

    /// <summary>
    /// Module is the most outer shell of gui object that load and unload
    /// data from data source. The behavior is of maximum abstraction.
    /// </summary>
    /// <remarks>
    /// Compatible with previous GameControllableEntity implementation.
    /// </remarks>
    public class Module<TModuleSettings> : GameControllableEntity, IModule
    {
        #region Constructors

        protected Module(TModuleSettings settings)
        {
            this.Settings = settings;
        }

        ~Module()
        {
            this.Dispose();
        }

        #endregion

        #region Components

        public IModuleLogicControl Logic { get; protected set; }

        public IModuleVisualControl Visual { get; protected set; }

        public TModuleSettings Settings { get; protected set; }

        #endregion

        #region Draw

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            this.Visual?.Draw(graphics, time, alpha);
        }

        #endregion
        
        #region Load and Unload

        public override void LoadContent(IGameInteropService interop)
        {
            this.Logic? .LoadContent(interop);
            this.Visual?.LoadContent(interop);

            base.LoadContent(interop);
        }

        public override void UnloadContent(IGameInteropService interop)
        {
            this.Logic?.UnloadContent(interop);

            base.UnloadContent(interop);
        }

        #endregion
        
        #region Update

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            this.Logic? .UpdateInput(input, time);
            this.Visual?.UpdateInput(input, time);
        }

        public override void Update(GameTime time)
        {
            base.Update(time);

            this.Logic? .Update(time);
            this.Visual?.Update(time);
        }

        #endregion
    }
}