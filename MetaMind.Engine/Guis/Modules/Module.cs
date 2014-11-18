using MetaMind.Engine.Guis.Widgets;
using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Guis.Modules
{
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
            Settings = settings;
        }

        public IModuleControl Control { get; protected set; }

        public IModuleGraphics Graphics { get; protected set; }

        public TModuleSettings Settings { get; protected set; }

        public override void Draw(GameTime gameTime, byte alpha)
        {
            Graphics.Draw(gameTime);
        }

        public override void HandleInput()
        {
            if (Control != null)
            {
                Control.HandleInput();
            }

            base.HandleInput();
        }

        public virtual void Load()
        {
            Control.Load();
        }

        public virtual void Unload()
        {
            Control.Unload();
        }

        public override void UpdateInput(GameTime gameTime)
        {
            Control.UpdateInput(gameTime);
        }

        public override void UpdateStructure( GameTime gameTime )
        {
            Control .UpdateStructure(gameTime);
            Graphics.Update(gameTime);
        }
    }
}