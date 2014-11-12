using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Extensions
{
    public static class PointExt
    {
        public static Vector2 DistanceFrom(this Point first, Point second)
        {
            return new Vector2(first.X, first.Y) - new Vector2(second.X, second.Y);
        }

        public static Vector2 ToVector2(this Point point)
        {
            return new Vector2(point.X, point.Y);
        }

        public static Rectangle PinRectangle(this Point position, Point size)
        {
            return new Rectangle(position.X, position.Y, size.X, size.Y);
        }

        public static Rectangle PinRectangleCenter(this Point center, Point size)
        {
            return new Point(center.X - size.X / 2, center.Y - size.Y / 2).PinRectangle(size);
        }
    }
}