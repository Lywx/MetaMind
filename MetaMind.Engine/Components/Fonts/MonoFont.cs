namespace MetaMind.Engine.Components.Fonts
{
    public class MonoFont
    {
        public float FontFactor = 2f / 3f;

        // This is a margin prefix to monospaced string to left-parallel normally spaced string
        public int FontMargin = 7;

        public MonoFont(Font font, int fontSize)
        {
            this.Font = font;
            this.FontSize = fontSize;
        }

        public Font Font { get; set; }

        public int FontSize { get; set; }

        public float AsciiSize(float scale)
        {
            return this.FontSize * this.FontFactor * scale;
        }
    }
}