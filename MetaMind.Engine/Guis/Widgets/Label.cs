namespace MetaMind.Engine.Guis.Widgets
{
    using System;

    using MetaMind.Engine.Components.Fonts;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public class Label : GameVisualEntity
    {
        public Func<string> Text { get; set; }

        public Func<Vector2> TextPosition { get; set; }

        public Func<Color> TextColor { get; set; }

        public Func<float> TextSide { get; set; }

        public Func<Font> TextFont { get; set; }

        protected Label(Func<string> text, Func<Vector2> textPosition, Func<Color> textColor, Func<float> textSide, Func<Font> textFont)
        {
            this.Text         = text;
            this.TextPosition = textPosition;
            this.TextSide     = textSide;
            this.TextColor    = textColor;
            this.TextFont     = textFont;
        }

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            graphics.String.DrawStringCenteredHV(
                this.TextFont(),
                this.Text(),
                this.TextPosition(),
                this.TextColor(),
                this.TextSide());
        }
    }
}