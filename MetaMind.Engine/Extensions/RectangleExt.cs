using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Extensions
{
    public static class RectangleExt
    {
        public static Rectangle Destination(this Rectangle rectangle)
        {
            return new Rectangle(rectangle.Center.X, rectangle.Center.Y, rectangle.Width, rectangle.Height);
        }

        public static Rectangle DestinationWithSize(this Rectangle rectangle, Point size)
        {
            return new Rectangle(rectangle.Center.X, rectangle.Center.Y, size.X, size.Y);
        }

        public static Rectangle DestinationWithOffset(this Rectangle rectangle, Point offset)
        {
            return new Rectangle(rectangle.Center.X + offset.X, rectangle.Center.Y + offset.Y, rectangle.Width, rectangle.Height);
        }

        public static Rectangle Extend(this Rectangle rectangle, Point margin)
        {
            return new Rectangle(rectangle.X - margin.X, rectangle.Y - margin.Y, rectangle.Width + margin.X * 2, rectangle.Height + margin.Y * 2);
        }

        public static Rectangle Crop(this Rectangle rectangle, Point margin)
        {
            return new Rectangle(rectangle.X + margin.X, rectangle.Y + margin.Y, rectangle.Width - margin.X * 2, rectangle.Height - margin.Y * 2);
        }

        public static Rectangle Rectangle(int xCenter, int yCenter, int width, int height)
        {
            return new Rectangle(xCenter - width / 2, yCenter - height / 2, width, height);
        }
    }
}