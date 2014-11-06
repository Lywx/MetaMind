using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Extensions
{
    public static class Vector2Extension
    {
        public static Point ToPoint( this Vector2 vector2 )
        {
            return new Point( ( int ) vector2.X, ( int ) vector2.Y );
        }
    }
}