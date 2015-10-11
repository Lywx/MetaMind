namespace MetaMind.Engine.Entities
{
    using Microsoft.Xna.Framework;

    /// <summary>
    /// MVC Abstraction for MMEntity.
    /// </summary>
    /// <remarks>
    /// Compatible with previous GameControllableEntity implementation.
    /// </remarks>
    public class MMMVCEntity<TMVCSettings> : MMInputEntity, IMMMVCEntity
    {
        #region Constructors and Finalizer

        protected MMMVCEntity(TMVCSettings settings)
        {
            this.Settings = settings;
        }

        ~MMMVCEntity()
        {
            this.Dispose();
        }

        #endregion

        #region Components

        public IMMMVCEntityController Controller { get; protected set; }

        public IMMMVCEntityRenderer Renderer { get; protected set; }

        public TMVCSettings Settings { get; protected set; }

        #endregion

        #region Draw

        public override void Draw(GameTime time)
        {
            this.Renderer?.Draw(time);
        }

        #endregion
        
        #region Load and Unload

        public override void LoadContent()
        {
            this.Controller? .LoadContent();
            this.Renderer?.LoadContent();

            base.LoadContent();
        }

        public override void UnloadContent()
        {
            this.Controller?.UnloadContent();

            base.UnloadContent();
        }

        #endregion
        
        #region Update

        public override void UpdateInput(GameTime time)
        {
            this.Controller? .UpdateInput(time);
            this.Renderer?.UpdateInput(time);
        }

        public override void Update(GameTime time)
        {
            base.Update(time);

            this.Controller? .Update(time);
            this.Renderer?.Update(time);
        }

        #endregion
    }
}