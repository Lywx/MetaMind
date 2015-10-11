namespace Microsoft.Xna.Framework
{
    using MetaMind.Engine.Entities.Controls;

    public static class RectangleExtension
    {
        public static Rectangle Extend(this Rectangle rectangle, MMMargin margin)
        {
            return new Rectangle(rectangle.Left - margin.Left, rectangle.Top - margin.Top, rectangle.Width + margin.Horizontal, rectangle.Height + margin.Vertical);
        }

        public static Rectangle Crop(this Rectangle rectangle, MMMargin margin)
        {
            return new Rectangle(rectangle.Left + margin.Left, rectangle.Top + margin.Top, rectangle.Width - margin.Horizontal, rectangle.Height - margin.Vertical);
        }

        public static Rectangle RectangleByCenter(int centerOfX, int centerOfY, int width, int height)
        {
            return new Rectangle(centerOfX - width / 2, centerOfY - height / 2, width, height);
        }

        public static Rectangle RectangleByCenter(Vector2 center, Vector2 size)
        {
            return RectangleByCenter((int)center.X, (int)center.Y, (int)size.X, (int)size.Y);
        }
    }
}