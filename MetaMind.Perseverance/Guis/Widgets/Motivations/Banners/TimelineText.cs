﻿namespace MetaMind.Perseverance.Guis.Widgets.Motivations.Banners
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
        private Font    font;

        public TimelineText(string name, Vector2 position, float size, Font font)
        {
            this.name     = name;
            this.position = position;
            this.size     = size;
            this.font     = font;
        }

        public void Draw(GameTime gameTime, byte alpha)
        {
            FontManager.DrawText(font, this.name, this.position, Color.White.MakeTransparent(alpha), this.size);
        }

        public void Update(GameTime gameTime)
        {
        }
    }
}