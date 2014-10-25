using MetaMind.Engine.Guis.Widgets;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis.Modules
{
    public interface IModule : IWidget
    {
        IModuleControl Control { get; }
        IModuleGraphics Graphics { get; }

        void Load( IModuleData data );
        void Unload();
        void Reload( IModuleData data );
    }

    /// <summary>
    /// Compatible with previous InputObject implementation, as long as
    /// the derived class override the widget implementation.
    /// </summary>
    public class Module<TModuleSettings> : InputObject, IModule
    {
        public IModuleControl Control { get; protected set; }
        public IModuleGraphics Graphics { get; protected set; }
        public TModuleSettings Settings { get; protected set; }

        public override void Draw( GameTime gameTime )
        {
            Graphics.Draw( gameTime );
        }

        public void Load(IModuleData data)
        {
            Control.Load( data ); 
        }

        public override void HandleInput()
        {
            if ( Control == null ) 
                return;

            base   .HandleInput();
            Control.HandleInput();
        }

        public void Unload()
        {
            Control.Unload();
        }

        public void Reload( IModuleData data )
        {
            Control.Load( data );
        }

        public override void UpdateInput( GameTime gameTime )
        {
            Control.UpdateInput( gameTime );
        }

        public override void UpdateStructure( GameTime gameTime )
        {
            Control .UpdateStructure( gameTime );
            Graphics.Update( gameTime );
        }
    }
}