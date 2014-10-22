using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Extensions
{
    public static class ColorExtension
    {
        public static Color DecrementBrightnessBy(this Color color, int value)
        {
            if (color.R <= color.G && color.R <= color.B)
                color.R -= (byte)value;
            else if (color.G <= color.R && color.G <= color.B)
                color.G -= (byte)value;
            else
                color.B -= (byte)value;
            return color;
        }

        public static Color IncrementBrightnessBy(this Color color, int value)
        {
            if (color.R >= color.G && color.R >= color.B)
                color.R += (byte)value;
            else if (color.G >= color.R && color.G >= color.B)
                color.G += (byte)value;
            else
                color.B += (byte)value;
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
    }
}