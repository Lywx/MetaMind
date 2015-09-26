namespace MetaMind.Engine.Components.Content.Fonts
{
    using System.Collections.Generic;
    using Extensions;
    using Microsoft.Xna.Framework;

    public static class FontExt
    {
        #region Measure

        public static Vector2 MeasureString(this Font font, string text) => font.SpriteData.MeasureString(text);

        public static Vector2 MeasureString(
            this Font font,
            string text,
            float scale)
        {
            return font.MeasureString(text) * scale;
        }

        public static Vector2 MeasureMonospacedString(
            this Font font,
            string str,
            float scale)
        {
            var cjkCharCount = str.CJKUniqueCharCount();
            var asciiCharCount = str.Length - cjkCharCount;

            var monoFont = font.MonoData;
            var monoSize = monoFont.AsciiSize(scale);

            return new Vector2(
                (asciiCharCount + cjkCharCount * 2) * monoSize.X,
                monoSize.Y);
        }

        public static Vector2 MeasureString(
            this Font font,
            string str,
            float scale,
            bool monospaced)
        {
            if (monospaced)
            {
                return font.MeasureMonospacedString(str, scale);
            }

            return font.MeasureString(str, scale);
        }

        #endregion

        #region Available

        public static bool Available(this Font font, char c) => font.SpriteData.Characters.Contains(c);

        public static string AvailableString(this Font font, string str) => font.SpriteData.AvailableString(str);

        public static List<int> UnavailableCharIndexes(this Font font, string str) => font.SpriteData.UnavailableCharIndexes(str);

        #endregion
    }
}