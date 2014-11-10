using MetaMind.Engine.Guis.Widgets;
using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Guis.Modules
{
    public interface IModule : IWidget
    {
        IModuleControl Control { get; }
        IModuleGraphics Graphics { get; }

        void Load( dynamic data );
        void Reload( dynamic data );

        void Unload();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// Compatible with previous Widget implementation, as long as
    /// the derived class override the widget implementation.
    /// </remarks>>
    public class Module<TModuleSettings> : Widget, IModule
    {
        public IModuleControl Control { get; protected set; }
        public IModuleGraphics Graphics { get; protected set; }
        public TModuleSettings Settings { get; protected set; }

        public override void Draw( GameTime gameTime, byte alpha )
        {
            Graphics.Draw( gameTime );
        }

        public override void HandleInput()
        {
            if ( Control == null )
                return;

            base   .HandleInput();
            Control.HandleInput();
        }

        public void Load( dynamic data )
        {
            Control.Load( data ); 
        }
        public void Reload( dynamic data )
        {
            Control.Load( data );
        }

        public void Unload()
        {
            Control.Unload();
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