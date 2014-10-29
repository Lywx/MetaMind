using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Extensions
{
    public static class PointExtension
    {
        public static Vector2 DistanceFrom( this Point first, Point second )
        {
            return new Vector2( first.X, first.Y ) - new Vector2( second.X, second.Y );
        }

        public static Vector2 ToVector2( this Point point )
        {
            return new Vector2( point.X, point.Y );
        }

        public static Rectangle ToRectangle( this Point position, Point size )
        {
            return new Rectangle( position.X, position.Y, size.X, size.Y );
        }
    }
}