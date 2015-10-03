namespace MetaMind.Engine.Entities
{
    using Microsoft.Xna.Framework;
    using Service;

    /// <summary>
    /// MVC Abstraction for MMEntity.
    /// </summary>
    /// <remarks>
    /// Compatible with previous GameControllableEntity implementation.
    /// </remarks>
    public class MMMvcEntity<TMvcSettings> : MMInputEntity, IMMMvcEntity
    {
        #region Constructors and Finalizer

        protected MMMvcEntity(TMvcSettings settings)
        {
            this.Settings = settings;
        }

        ~MMMvcEntity()
        {
            this.Dispose();
        }

        #endregion

        #region Components

        public IMMMvcEntityLogic Logic { get; protected set; }

        public IMMMvcEntityVisual Visual { get; protected set; }

        public TMvcSettings Settings { get; protected set; }

        #endregion

        #region Draw

        public override void Draw(IMMEngineGraphicsService graphics, GameTime time, byte alpha)
        {
            this.Visual?.Draw(graphics, time, alpha);
        }

        #endregion
        
        #region Load and Unload

        public override void LoadContent(IMMEngineInteropService interop)
        {
            this.Logic? .LoadContent(interop);
            this.Visual?.LoadContent(interop);

            base.LoadContent(interop);
        }

        public override void UnloadContent(IMMEngineInteropService interop)
        {
            this.Logic?.UnloadContent(interop);

            base.UnloadContent(interop);
        }

        #endregion
        
        #region Update

        public override void UpdateInput(IMMEngineInputService input, GameTime time)
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