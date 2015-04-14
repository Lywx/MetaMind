namespace MetaMind.Engine.Guis
{
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
        protected Module(TModuleSettings settings)
        {
            this.Settings = settings;
        }

        public IModuleControl Control { get; protected set; }

        public IModuleGraphics Graphics { get; protected set; }

        public TModuleSettings Settings { get; protected set; }

        public override void Draw(IGameGraphics gameGraphics, GameTime gameTime, byte alpha)
        {
            this.Graphics.Draw(gameGraphics, gameTime, alpha);
        }

        public virtual void Load(IGameFile gameFile, IGameInput gameInput, IGameInterop gameInterop, IGameAudio gameAudio)
        {
            this.Control.Load(gameFile, gameInput, gameInterop, gameAudio);
        }

        public virtual void Unload(IGameFile gameFile, IGameInput gameInput, IGameInterop gameInterop, IGameAudio gameAudio)
        {
            this.Control.Unload(gameFile, gameInput, gameInterop, gameAudio);
        }

        public override void UpdateInput(IGameInput gameInput, GameTime gameTime)
        {
            this.Control .UpdateInput(gameInput, gameTime);
            this.Graphics.UpdateInput(gameInput, gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            this.Control .Update(gameTime);
            this.Graphics.Update(gameTime);
        }
    }
}