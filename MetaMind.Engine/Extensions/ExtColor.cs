// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExtColor.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Extensions
{
    using System;

    using Microsoft.Xna.Framework;

    public static class ExtColor
    {
        public static Color DecrementBrightnessBy(this Color color, int value)
        {
            if (color.R <= color.G && color.R <= color.B)
            {
                color.R -= (byte)value;
            }
            else if (color.G <= color.R && color.G <= color.B)
            {
                color.G -= (byte)value;
            }
            else
            {
                color.B -= (byte)value;
            }

            return color;
        }

        public static Color IncrementBrightnessBy(this Color color, int value)
        {
            if (color.R >= color.G && color.R >= color.B)
            {
                color.R += (byte)value;
            }
            else if (color.G >= color.R && color.G >= color.B)
            {
                color.G += (byte)value;
            }
            else
            {
                color.B += (byte)value;
            }

            return color;
        }

        public static Color MakeTransparent(this Color color, byte alpha)
        {
            color.A = (byte)(alpha * color.A / 255);
            color.R = (byte)(alpha * color.R / 255);
            color.G = (byte)(alpha * color.G / 255);
            color.B = (byte)(alpha * color.B / 255);
            return color;
        }

        private static void HsvToRgb(float h, float s, float v, out float r, out float g, out float b)
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

        private static void RgbToHsv(float r, float g, float b, out float h, out float s, out float v)
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