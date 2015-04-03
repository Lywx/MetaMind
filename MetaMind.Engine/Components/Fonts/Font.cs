namespace MetaMind.Engine.Components.Fonts
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public enum Font
    {
        UiRegularFont,

        UiStatisticsFont,

        UiContentFont,

        // ---------------------------------------------------------------------
        FontNum,
    }

    public static class FontExt
    {
        public static Vector2 MeasureString(this Font font, string text)
        {
            return GameEngine.FontManager[font].MeasureString(text);
        }

        public static Vector2 MeasureString(this Font font, string text, float scale)
        {
            return font.MeasureString(text) * scale;
        }

    }

    public static class SpriteFontExt
    {
        public static Vector2 MesureString(this SpriteFont spriteFont, string text, float scale)
        {
            return spriteFont.MeasureString(text) * scale;
        }
    }
}