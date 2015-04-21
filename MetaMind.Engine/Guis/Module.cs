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

        public virtual void Load(IGameInputService input, IGameInteropService interop)
        {
            this.Control.Load(input, interop);
        }

        public virtual void Unload(IGameInputService input, IGameInteropService interop)
        {
            this.Control.Unload(input, interop);
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            this.Control .UpdateInput(input, time);
            this.Graphics.UpdateInput(input, time);
        }

        public override void Update(GameTime time)
        {
            this.Control .Update(time);
            this.Graphics.Update(time);
        }
    }
}