using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Extensions
{
    public static class RectangleExtension
    {
        public static Rectangle Destination( this Rectangle rectangle )
        {
            return new Rectangle( rectangle.Center.X, rectangle.Center.Y, rectangle.Width, rectangle.Height );
        }

        public static Rectangle DestinationWithSize( this Rectangle rectangle, Point size )
        {
            return new Rectangle( rectangle.Center.X, rectangle.Center.Y, size.X, size.Y );
        }

        public static Rectangle DestinationWithOffset( this Rectangle rectangle, Point offset )
        {
            return new Rectangle( rectangle.Center.X + offset.X, rectangle.Center.Y + offset.Y, rectangle.Width, rectangle.Height );
        }
    }
}