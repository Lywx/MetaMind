using MetaMind.Engine;
using MetaMind.Engine.Components;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis.Widgets.Motivations.Banners
{
    public class TimelineText : EngineObject
    {
        private string  name;
        private Vector2 position;

        public TimelineText( string name, Vector2 position )
        {
            this.name     = name;
            this.position = position;
        }

        public void Draw( GameTime gameTime )
        {
            FontManager.DrawText( Font.UiRegularFont, name, position, Color.White, 1f );
        }

        public void Update( GameTime gameTime )
        {
        }
    }
}