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

        public Func<float> TextSize { get; set; }

        public Func<Font> TextFont { get; set; }

        public StringHAlign TextHAlign { get; set; }

        public StringVAlign TextVAlign { get; set; }

        public bool TextMonospaced { get; set; }

        public Label(Func<Font> textFont, Func<string> text, Func<Vector2> textPosition, Func<Color> textColor, Func<float> textSize, StringHAlign textHAlign, StringVAlign textVAlign, bool textMonospaced)
            : this(textFont, text, textPosition, textColor, textSize)
        {
            this.TextHAlign     = textHAlign;
            this.TextVAlign     = textVAlign;
            this.TextMonospaced = textMonospaced;
        }

        public Label(Func<Font> textFont, Func<string> text, Func<Vector2> textPosition, Func<Color> textColor, Func<float> textSize)
        {
            this.Text         = text;
            this.TextPosition = textPosition;
            this.TextSize     = textSize;
            this.TextColor    = textColor;
            this.TextFont     = textFont;

            this.TextHAlign = StringHAlign.Right;
            this.TextVAlign = StringVAlign.Bottom;

            this.TextMonospaced = false;
        }

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            var font     = this.TextFont();
            var text     = this.Text();
            var position = this.TextPosition();
            var color    = this.TextColor();
            var size     = this.TextSize();

            var drawer = graphics.StringDrawer;

            var draw = this.TextMonospaced
                           ? (Action<Font, string, Vector2, Color, float, StringHAlign, StringVAlign>)
                             drawer.DrawMonospacedString
                           : drawer.DrawString;

            draw(font, text, position, color.MakeTransparent(alpha), size, this.TextHAlign, this.TextVAlign);
        }
    }
}