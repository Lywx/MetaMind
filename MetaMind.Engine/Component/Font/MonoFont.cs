namespace MetaMind.Engine.Component.Font
{
    using Microsoft.Xna.Framework;

    public class MonoFont
    {
        // This is the width / height ratio
        public float FontFactor = 73f / 102f;

        // This is a margin prefix to monospaced string to left-parallel normally spaced string
        public int FontMargin = 7;

        public MonoFont(Font font, int fontSize)
        {
            this.Font = font;
            this.FontSize = fontSize;
        }

        public Font Font { get; set; }

        public int FontSize { get; set; }

        public Vector2 AsciiSize(float scale)
        {
            return new Vector2(this.FontSize * this.FontFactor * scale, this.FontSize * scale);
        }
    }
}