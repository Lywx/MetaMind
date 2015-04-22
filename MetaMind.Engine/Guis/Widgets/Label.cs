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

        public Label(Func<string> text, Func<Vector2> textPosition, Func<Color> textColor, Func<float> textSize, Func<Font> textFont)
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

            var drawer = graphics.String;

            var draw = this.TextMonospaced
                           ? (Action<Font, string, Vector2, Color, float, StringHAlign, StringVAlign>)
                             drawer.DrawMonospacedString
                           : drawer.DrawString;

            draw(font, text, position, color, size, this.TextHAlign, this.TextVAlign);
        }
    }
}