namespace MetaMind.Engine.Gui.Modules
{
    using Microsoft.Xna.Framework;
    using Service;

    /// <summary>
    /// MVC Abstraction for GameEntity.
    /// </summary>
    /// <remarks>
    /// Compatible with previous GameControllableEntity implementation.
    /// </remarks>
    public class GameMvcEntity<TMvcSettings> : GameInputableEntity, IGameMvcEntity
    {
        #region Constructors and Finalizer

        protected GameMvcEntity(TMvcSettings settings)
        {
            this.Settings = settings;
        }

        ~GameMvcEntity()
        {
            this.Dispose();
        }

        #endregion

        #region Components

        public IGameMvcEntityLogic Logic { get; protected set; }

        public IGameMvcEntityVisual Visual { get; protected set; }

        public TMvcSettings Settings { get; protected set; }

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