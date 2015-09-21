namespace MetaMind.Engine.Component.Content.Font
{
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public static class FontAssetExt
    {
        #region Measure

        public static Vector2 MeasureString(this FontAsset font, string text) => font.SpriteData.MeasureString(text);

        public static Vector2 MeasureString(
            this FontAsset font,
            string text,
            float scale)
        {
            return font.MeasureString(text) * scale;
        }

        public static Vector2 MeasureMonospacedString(
            this FontAsset font,
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
            this FontAsset font,
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

        public static bool Available(this FontAsset font, char c) => font.SpriteData.Characters.Contains(c);

        public static string AvailableString(this FontAsset font, string str) => font.SpriteData.AvailableString(str);

        public static List<int> UnavailableCharIndexes(this FontAsset font, string str) => font.SpriteData.UnavailableCharIndexes(str);

        #endregion
    }
}