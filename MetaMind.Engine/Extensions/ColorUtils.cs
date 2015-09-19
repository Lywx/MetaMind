namespace Microsoft.Xna.Framework
{
    using System;

    public static class ColorUtils
    {
        public static Color Parse(string str)
        {
            var value = str.Split(',');

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

            return Color.FromNonPremultiplied(r, g, b, a);
        }

        private static void HSVToRGB(float h, float s, float v, out float r, out float g, out float b)
        {
            // Keeps h from going over 360
            h = h - ((int)(h / 360) * 360);

            const float tolerance = 0.1f;

            if (Math.Abs(s) < tolerance)
            {
                // achromatic (grey)
                r = g = b = v;
                return;
            }

            // sector 0 to 5
            h /= 60;

            var i = (int)h;
            var f = h - i;
            var p = v * (1 - s);
            var q = v * (1 - s * f);
            var t = v * (1 - s * (1 - f));
            switch (i)
            {
                case 0:
                    r = v;
                    g = t;
                    b = p;
                    break;

                case 1:
                    r = q;
                    g = v;
                    b = p;
                    break;

                case 2:
                    r = p;
                    g = v;
                    b = t;
                    break;

                case 3:
                    r = p;
                    g = q;
                    b = v;
                    break;

                case 4:
                    r = t;
                    g = p;
                    b = v;
                    break;

                // case 5
                default:
                    r = v;
                    g = p;
                    b = q;
                    break;
            }
        }

        private static void RGBToHSV(float r, float g, float b, out float h, out float s, out float v)
        {
            const float tolerance = 0.1f;

            var min = Math.Min(Math.Min(r, g), b);
            var max = Math.Max(Math.Max(r, g), b);
            v = max;
            var delta = max - min;
            if (Math.Abs(max) > tolerance)
            {
                s = delta / max;

                if (Math.Abs(r - max) < tolerance)
                {
                    // between yellow and magenta
                    h = (g - b) / delta;
                }
                else if (Math.Abs(g - max) < tolerance)
                {
                    // between cyan and yellow
                    h = 2 + (b - r) / delta;
                }
                else
                {
                    // between magenta and cyan
                    h = 4 + (r - g) / delta;
                }

                // degrees
                h *= 60;
                if (h < 0)
                {
                    h += 360;
                }
            }
            else
            {
                // r = g = b = 0
                // s = 0, v is undefined
                s = 0;
                h = -1;
            }
        }
        
    }
}