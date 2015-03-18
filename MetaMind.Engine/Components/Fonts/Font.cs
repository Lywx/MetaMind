namespace MetaMind.Engine.Components.Fonts
{
    using Microsoft.Xna.Framework;

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
    }
}