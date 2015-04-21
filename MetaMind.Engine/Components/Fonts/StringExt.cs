// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringExt.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Components.Fonts
{
    using System.Collections.Generic;
    using System.Text;

    using Microsoft.Xna.Framework;

    public static class StringExt
    {
        public static int CJKExclusiveCharCount(this string str)
        {
            return str.CJKExclusiveCharIndexes().Count;
        }

        public static List<int> CJKExclusiveCharIndexes(this string str)
        {
            // HACK: May not use standard font here.
            return Font.UiRegular.NonPrintableCharIndexes(str);
        }

        public static string CJKInclusiveString(this string str)
        {
            return Font.ContentRegular.PrintableString(str);
        }

        public static Vector2 MeasureMonospacedString(this string str, float scale)
        {
            // HACK: May not use standard font here.
            return Font.ContentRegular.MeasureMonospacedString(str, scale);
        }

        public static bool IsAscii(this string value)
        {
            // ASCII encoding replaces non-ascii with question marks, so we use UTF8 to see if multi-byte sequences are there
            return Encoding.UTF8.GetByteCount(value) == value.Length;
        }
    }
}