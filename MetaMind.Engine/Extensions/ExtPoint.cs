// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExtPoint.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Extensions
{
    using Microsoft.Xna.Framework;

    public static class ExtPoint
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