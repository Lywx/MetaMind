using MetaMind.Engine.Guis.Elements.Abstract;
using MetaMind.Engine.Extensions;
using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Guis.Elements.Frames
{
    public static class PickableFrameExtension
    {
        public static Rectangle DestinationWithSize( this IPickableFrame frame, Point size )
        {
            return frame.Rectangle.DestinationWithSize( size );
        }

        public static Rectangle DestinationWithOffset( this IPickableFrame frame, Point offset )
        {
            return frame.Rectangle.DestinationWithOffset( offset );
        }

        public static Rectangle Destination( this IPickableFrame frame )
        {
            return frame.Rectangle.Destination();
        }
    }

    public interface IPickableFrame : IPressableFrame, IPickable
    {
    }
}