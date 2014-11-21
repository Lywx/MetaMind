namespace MetaMind.Perseverance.Guis.Widgets.Motivations.Banners
{
    using MetaMind.Engine;
    using MetaMind.Engine.Components.Fonts;
    using MetaMind.Engine.Extensions;

    using Microsoft.Xna.Framework;

    public class TimelineText : EngineObject
    {
        private string  name;
        private Vector2 position;
        private float   size;

        public TimelineText(string name, Vector2 position, float size)
        {
            this.name     = name;
            this.position = position;
            this.size     = size;
        }

        public void Draw(GameTime gameTime, byte alpha)
        {
            FontManager.DrawText(Font.UiRegularFont, this.name, this.position, Color.White.MakeTransparent(alpha), this.size);
        }

        public void Update(GameTime gameTime)
        {
        }
    }
}