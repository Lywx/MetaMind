namespace MetaMind.Engine.Guis.Elements
{
    using MetaMind.Engine.Extensions;

    using Microsoft.Xna.Framework;

    // FIXME: Really need it?
    public static class PickableFrameExt
    {
        public static Rectangle DestinationWithSize(this IPickableFrame frame, Point size)
        {
            return frame.Rectangle.DestinationWithSize(size);
        }

        public static Rectangle DestinationWithOffset(this IPickableFrame frame, Point offset)
        {
            return frame.Rectangle.DestinationWithOffset(offset);
        }

        public static Rectangle Destination(this IPickableFrame frame)
        {
            return frame.Rectangle.Destination();
        }
    }
}