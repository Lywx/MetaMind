namespace Microsoft.Xna.Framework
{
    public static class ColorUtils
    {
        #region Hex Converter

        public static Color FromHex(uint hex)
        {
            var a = (int) (hex               >> 24);
            var r = (int)((hex & 0x00ffffff) >> 16);
            var g = (int)((hex & 0x0000ffff) >> 8);
            var b = (int) (hex & 0x000000ff);

            return Color.FromNonPremultiplied(r, g, b, a);
        }

        public static uint ToHex(Color color)
        {
            return ((uint)color.A << 24) + ((uint)255 << color.R) + ((uint)255 << color.G) + color.B;
        }

        #endregion
    }
}
