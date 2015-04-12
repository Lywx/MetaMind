namespace MetaMind.Engine.Guis
{
    using Microsoft.Xna.Framework;

    /// FIXME: Won't work now.
    /// <summary>
    /// Module is the most outer shell of gui object that load and unload
    /// data from data source. The behavior is of maximum abstraction.
    /// </summary>
    /// <remarks>
    /// Compatible with previous InputableGameEntity implementation, as long as
    /// the derived class override the widget implementation.
    /// </remarks>>
    public class Module<TModuleSettings> : InputableGameEntity, IModule
    {
        protected Module(TModuleSettings settings)
        {
            this.Settings = settings;
        }

        public IModuleControl Control { get; protected set; }

        public IModuleGraphics Graphics { get; protected set; }

        public TModuleSettings Settings { get; protected set; }

        public override void Draw(IGameGraphics gameGraphics, GameTime gameTime, byte alpha)
        {
            this.Graphics.Draw(gameTime);
        }

        public virtual void Load(IGameFile gameFile, IGameInput gameInput, IGameInterop gameInterop, IGameSound gameSound)
        {
            this.Control.Load(gameFile, gameInput, gameInterop, gameSound);
        }

        public virtual void Unload(IGameFile gameFile, IGameInput gameInput, IGameInterop gameInterop, IGameSound gameSound)
        {
            this.Control.Unload(gameFile, gameInput, gameInterop, gameSound);
        }

        public override void Update(IGameInput gameInput, GameTime gameTime)
        {
            this.Control .Update(gameInput, gameTime);
            this.Graphics.Update(gameInput, gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            this.Control .Update(gameTime);
            this.Graphics.Update(gameTime);
        }
    }
}