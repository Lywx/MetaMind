namespace MetaMind.Engine.Extensions
{
    using MetaMind.Engine.Components.Fonts;

    using Microsoft.Xna.Framework;

    internal static class ExtFont
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
}