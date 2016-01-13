namespace MetaMind.Engine.Services.IO
{
    using Microsoft.Xna.Framework;
    using System;
    using TrueColor = Microsoft.Xna.Framework.Color;

    public static class XmlAttributeFormatter
    {
        public static class Color
        {
            #region RGBA Format

            public static string ToRgbaString(TrueColor value)
            {
                return $"{value.R}, {value.G}, {value.B}, {value.A}";
            }

            public static TrueColor ParseRgba(string valueString)
            {
                var value = valueString.Split(',');

                byte r = 255, g = 255, b = 255, a = 255;

                if (value.Length >= 1)
                {
                    r = byte.Parse(value[0]);
                }

                if (value.Length >= 2)
                {
                    g = byte.Parse(value[1]);
                }

                if (value.Length >= 3)
                {
                    b = byte.Parse(value[2]);
                }

                if (value.Length >= 4)
                {
                    a = byte.Parse(value[3]);
                }

                return TrueColor.FromNonPremultiplied(r, g, b, a);
            }

            #endregion

            #region ARBG Hex Format

            public static string ToArgbHexString(TrueColor value)
            {
                var aHexString = value.A.ToString("X2");
                var rHexString = value.R.ToString("X2");
                var gHexString = value.G.ToString("X2");
                var bHexString = value.B.ToString("X2");

                return $"#{aHexString}{rHexString}{gHexString}{bHexString}";
            }

            public static TrueColor ParseArgbHex(string valueString)
            {
                var hex = Convert.ToUInt32(valueString, 16);

                return ColorUtils.FromHex(hex);
            }

            #endregion
        }
    }
}