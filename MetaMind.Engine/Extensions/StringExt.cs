namespace MetaMind.Engine.Extensions
{
    using System.Collections.Generic;
    using System.Text;
    using Component.Content.Fonts;
    using Microsoft.Xna.Framework;

    public static class StringExt
    {
        public static Vector2 MeasureMonospacedString(this string str, float scale)
        {
            // TODO(Minor): May not use standard font here.
            return NSimSunRegularFont.MeasureMonospacedString(str, scale);
        }

        #region Characters

        public static int CJKUniqueCharCount(this string str)
        {
            return str.CJKUniqueCharIndexes().Count;
        }

        public static List<int> CJKUniqueCharIndexes(this string str)
        {
            // TODO(Minor): May not use standard font here.
            return LucidaConsoleRegularFont.UnavailableCharIndexes(str);
        }

        public static string CJKAvailableString(this string str)
        {
            return NSimSunRegularFont.AvailableString(str);
        }

        public static bool IsAscii(this string value)
        {
            // ASCII encoding replaces non-ascii with question marks, so we use UTF8 to see if multi-byte sequences are there
            return Encoding.UTF8.GetByteCount(value) == value.Length;
        }

        #endregion

        #region Initialization

        public static void Initialize(IFontManager fonts)
        {
            NSimSunRegularFont       = fonts["NSimSum Regular"];
            LucidaConsoleRegularFont = fonts["Lucida Console Regular"];
        }

        private static Font NSimSunRegularFont { get; set; }

        private static Font LucidaConsoleRegularFont { get; set; }

        #endregion
    }
}