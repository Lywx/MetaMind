namespace MetaMind.Engine.Components.Fonts
{
    using System;
    using System.Collections.Generic;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public static class FontExt
    {
        private static IFontManager fontManager;

        #region Access

        public static SpriteFont Sprite(this Font font)
        {
            return font.Info().SpriteFont;
        }

        public static FontInfo Info(this Font font)
        {
            return fontManager.Fonts[font];
        }

        public static MonoFont Mono(this Font font)
        {
            return font.Info().MonoFont;
        }

        #endregion

        #region Measure

        public static Vector2 MeasureString(this Font font, string text)
        {
            return font.Sprite().MeasureString(text);
        }

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
            var cjkCharCount = str.CJKExclusiveCharCount();
            var asciiCharCount = str.Length - cjkCharCount;

            var monoFont = font.Mono();
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

        #region Printable

        public static bool IsPrintable(this Font font, char c)
        {
            return font.Sprite().Characters.Contains(c);
        }

        public static string PrintableString(this Font font, string str)
        {
            return font.Sprite().PrintableString(str);
        }

        public static List<int> NonPrintableCharIndexes(
            this Font font,
            string str)
        {
            return font.Sprite().NonPrintableCharIndexes(str);
        }

        #endregion

        public static void Initialize(IFontManager fontManager)
        {
            if (fontManager == null)
            {
                throw new ArgumentNullException(nameof(fontManager));
            }

            FontExt.fontManager = fontManager;
        }
    }
}