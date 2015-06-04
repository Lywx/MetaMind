namespace MetaMind.Engine.Guis.Widgets.Visuals
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

        public Func<float> TextSize { get; set; }

        public Func<Font> TextFont { get; set; }

        public StringHAlign TextHAlign { get; set; }

        public StringVAlign TextVAlign { get; set; }

        public int TextLeading { get; set; }

        public bool TextMonospaced { get; set; }

        public Label(Func<Font> textFont, Func<string> text, Func<Vector2> textPosition, Func<Color> textColor, Func<float> textSize, StringHAlign textHAlign, StringVAlign textVAlign, int textLeading=0, bool textMonospaced=false)
            : this(textFont, text, textPosition, textColor, textSize)
        {
            this.TextHAlign     = textHAlign;
            this.TextVAlign     = textVAlign;
            this.TextLeading    = textLeading;
            this.TextMonospaced = textMonospaced;
        }

        public Label(Func<Font> textFont, Func<string> text, Func<Vector2> textPosition, Func<Color> textColor, Func<float> textSize)
        {
            if (text == null)
            {
                throw new ArgumentNullException("text");
            }

            if (textPosition == null)
            {
                throw new ArgumentNullException("textPosition");
            }

            if (textSize == null)
            {
                throw new ArgumentNullException("textSize");
            }

            if (textFont == null)
            {
                throw new ArgumentNullException("textFont");
            }

            this.Text         = text;
            this.TextPosition = textPosition;
            this.TextSize     = textSize;
            this.TextColor    = textColor;
            this.TextFont     = textFont;

            this.TextHAlign = StringHAlign.Right;
            this.TextVAlign = StringVAlign.Bottom;

            this.TextLeading = 0;

            this.TextMonospaced = false;
        }

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            var drawer = graphics.StringDrawer;

            var draw = this.TextMonospaced
                           ? (Action<Font, string, Vector2, Color, float, StringHAlign, StringVAlign, int>)
                             ((font, str, position, color, scale, hAlign, vAlign, leading) => drawer.DrawMonospacedString(font, str, position, color, scale, hAlign, vAlign, leading))
                           : ((font, str, position, color, scale, hAlign, vAlign, leading) => drawer.DrawString(font, str, position, color, scale, hAlign, vAlign, leading));

            draw(
                this.TextFont(),
                this.Text(),
                this.TextPosition(),
                this.TextColor().MakeTransparent(alpha),
                this.TextSize(),
                this.TextHAlign,
                this.TextVAlign,
                this.TextLeading);
        }
    }
}