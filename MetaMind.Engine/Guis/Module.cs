namespace MetaMind.Engine.Guis
{
    using MetaMind.Engine.Guis.Widgets;

    using Microsoft.Xna.Framework;

    public interface IModule : IWidget
    {
        IModuleControl Control { get; }

        IModuleGraphics Graphics { get; }

        void Load();

        void Unload();
    }

    /// <summary>
    /// Module is the most outer shell of gui object that load and unload
    /// data from data source. The behavior is of maximum abstraction.
    /// </summary>
    /// <remarks>
    /// Compatible with previous Widget implementation, as long as
    /// the derived class override the widget implementation.
    /// </remarks>>
    public class Module<TModuleSettings> : Widget, IModule
    {
        protected Module(TModuleSettings settings)
        {
            this.Settings = settings;
        }

        public IModuleControl Control { get; protected set; }

        public IModuleGraphics Graphics { get; protected set; }

        public TModuleSettings Settings { get; protected set; }

        public override void Draw(GameTime gameTime, byte alpha)
        {
            this.Graphics.Draw(gameTime);
        }

        public override void HandleInput()
        {
            if (this.Control != null)
            {
                this.Control.HandleInput();
            }

            base.HandleInput();
        }

        public virtual void Load()
        {
            this.Control.Load();
        }

        public virtual void Unload()
        {
            this.Control.Unload();
        }

        public override void UpdateInput(GameTime gameTime)
        {
            this.Control.UpdateInput(gameTime);
        }

        public override void UpdateStructure(GameTime gameTime)
        {
            this.Control .UpdateStructure(gameTime);
            this.Graphics.Update(gameTime);
        }
    }
}