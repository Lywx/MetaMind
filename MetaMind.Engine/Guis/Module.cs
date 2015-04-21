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
        protected Module(TModuleSettings settings)
        {
            this.Settings = settings;
        }

        public IModuleControl Control { get; protected set; }

        public IModuleGraphics Graphics { get; protected set; }

        public TModuleSettings Settings { get; protected set; }

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            this.Graphics.Draw(graphics, time, alpha);
        }

        public virtual void Load(IGameFile gameFile, IGameInputService input, IGameInteropService interop, IGameAudioService audio)
        {
            this.Control.Load(gameFile, input, interop, audio);
        }

        public virtual void Unload(IGameFile gameFile, IGameInputService input, IGameInteropService interop, IGameAudioService audio)
        {
            this.Control.Unload(gameFile, input, interop, audio);
        }

        public override void UpdateInput(IGameInputService input, GameTime gameTime)
        {
            this.Control .UpdateInput(input, gameTime);
            this.Graphics.UpdateInput(input, gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            this.Control .Update(gameTime);
            this.Graphics.Update(gameTime);
        }
    }
}