namespace MetaMind.Engine.Components.Fonts
{
    using System;
    using System.Collections.Generic;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public static class FontExt
    {
        private static IFontManager fontManager;

        public static string PrintableString(this Font font, string str)
        {
            return font.GetSprite().PrintableString(str);
        }

        public static List<int> NonPrintableCharIndexes(this Font font, string str)
        {
            return font.GetSprite().NonPrintableCharIndexes(str);
        }

        public static SpriteFont GetSprite(this Font font)
        {
            return font.GetInfo().SpriteFont;
        }

        public static FontInfo GetInfo(this Font font)
        {
            return fontManager.Fonts[font];
        }

        public static MonoFont GetMono(this Font font)
        {
            return font.GetInfo().MonoFont;
        }

        public static Vector2 MeasureString(this Font font, string text)
        {
            return font.GetSprite().MeasureString(text);
        }

        public static Vector2 MeasureString(this Font font, string text, float scale)
        {
            return font.MeasureString(text) * scale;
        }

        public static Vector2 MeasureMonospacedString(this Font font, string str, float scale)
        {
            var cjkCharCount = str.CJKExclusiveCharCount();
            var asciiCharCount = str.Length - cjkCharCount;

            var monoSize = font.GetMono().AsciiSize(scale);

            return new Vector2((asciiCharCount + cjkCharCount * 2) * monoSize, monoSize);
        }

        public static Vector2 MeasureString(this Font font, string str, float scale, bool monospaced)
        {
            if (monospaced)
            {
                return font.MeasureMonospacedString(str, scale);
            }

            return font.MeasureString(str, scale);
        }


        public static void Initialize(IFontManager fontManager)
        {
            if (fontManager == null)
            {
                throw new ArgumentNullException("fontManager");
            }

            FontExt.fontManager = fontManager;
        }
    }
}