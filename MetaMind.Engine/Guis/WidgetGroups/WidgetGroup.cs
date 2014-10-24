using MetaMind.Engine.Guis.Widgets;
using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Guis.WidgetGroups
{
    public abstract class WidgetGroup<TWidgetGroupSettings> : InputObject
    {
        public TWidgetGroupSettings Settings { get; protected set; }
        public IWidgetGroupControl Control { get; protected set; }
        public IWidgetGroupGraphics Graphics { get; protected set; }

        protected WidgetGroup(TWidgetGroupSettings settings)
        {
            Settings = settings;
        }

        public override void HandleInput()
        {
            base   .HandleInput();
            Control.HandleInput();
        }

        public override void Draw(GameTime gameTime)
        {
            Graphics.Draw( gameTime );
        }

        public override void UpdateInput( GameTime gameTime )
        {
            Control.UpdateInput( gameTime );
        }

        public override void UpdateStructure( GameTime gameTime )
        {
            Control.UpdateStructure( gameTime );
            Graphics.Update( gameTime );
        }
    }
}