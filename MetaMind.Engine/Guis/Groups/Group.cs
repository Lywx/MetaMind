using MetaMind.Engine.Guis.Widgets;
using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Guis.Groups
{
    public abstract class Group<TGroupSettings> : Widget
    {
        public TGroupSettings Settings { get; protected set; }
        public IGroupControl  Control  { get; protected set; }
        public IGroupGraphics Graphics { get; protected set; }

        protected Group( TGroupSettings settings )
        {
            Settings = settings;
        }

        public override void HandleInput()
        {
            base   .HandleInput();
            Control.HandleInput();
        }

        public override void Draw( GameTime gameTime, byte alpha )
        {
            Graphics.Draw( gameTime );
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