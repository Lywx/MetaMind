namespace MetaMind.Engine.Component.Font
{
    using Microsoft.Xna.Framework.Graphics;

    public class FontInfo
    {
        public Font Font { get; set; }

        public SpriteFont SpriteFont { get; set; }

        public MonoFont MonoFont { get; set; }

        public FontInfo(Font font, SpriteFont spriteFont, int fontSize)
        {
            this.Font       = font;
            this.SpriteFont = spriteFont;

            this.MonoFont = new MonoFont(font, fontSize);
        }
    }
}