namespace Microsoft.Xna.Framework.Graphics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal static class SpriteFontExt
    {
        public static string FilterDisaplayableString(this SpriteFont font, string str)
        {
            if (font == null)
            {
                throw new ArgumentNullException("font");
            }

            return str.Where(t => font.Characters.Contains(t)).Aggregate(string.Empty, (current, t) => current + t);
        }

        public static List<int> FilterNonDisaplayableCharIndexes(this SpriteFont spriteFont, string str)
        {
            if (spriteFont == null)
            {
                throw new ArgumentNullException("spriteFont");
            }

            var indexes = new List<int>();
            for (var i = 0; i < str.Length; i++)
            {
                if (!spriteFont.Characters.Contains(str[i]))
                {
                    indexes.Add(i);
                }
            }

            return indexes;
        }

        public static Vector2 MeasureString(this SpriteFont spriteFont, string text, float scale)
        {
            return spriteFont.MeasureString(text) * scale;
        }
    }
}