namespace MetaMind.Engine.Component.Content.Font
{
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Xna.Framework;

    public static class FontStringExt
    {
        public static Vector2 MeasureMonospacedString(this string str, float scale)
        {
            // FIXME: May not use standard font here.
            return "ContentRegular".ToFont().MeasureMonospacedString(str, scale);
        }

        #region Characters

        public static int CJKUniqueCharCount(this string str)
        {
            return str.CJKUniqueCharIndexes().Count;
        }

        public static List<int> CJKUniqueCharIndexes(this string str)
        {
            // FIXME: May not use standard font here.
            return "UiRegular".ToFont().UnavailableCharIndexes(str);
        }

        public static string CJKAvailableString(this string str)
        {
            return "ContentRegular".ToFont().AvailableString(str);
        }

        public static bool IsAscii(this string value)
        {
            // ASCII encoding replaces non-ascii with question marks, so we use UTF8 to see if multi-byte sequences are there
            return Encoding.UTF8.GetByteCount(value) == value.Length;
        }

        #endregion
    }
}