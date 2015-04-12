namespace MetaMind.Engine.Extensions
{
    // FIXME: Namespace
    using MetaMind.Engine.Components;
    using MetaMind.Engine.Components.Fonts;

    using Microsoft.Xna.Framework;

    internal static class ExtFont
    {
        private static FontManager FontManager
        {
            get { return GameEngine.GetInstance.FontManager; }
        }

        public static Vector2 MeasureString(this Font font, string text)
        {
            return FontManager[font].MeasureString(text);
        }

        public static Vector2 MeasureString(this Font font, string text, float scale)
        {
            return font.MeasureString(text) * scale;
        }
    }
}