namespace Microsoft.Xna.Framework
{
    public static class RectangleExtension
    {
        public static Rectangle Extend(this Rectangle rectangle, Point margin)
        {
            return new Rectangle(rectangle.X - margin.X, rectangle.Y - margin.Y, rectangle.Width + margin.X * 2, rectangle.Height + margin.Y * 2);
        }

        public static Rectangle Crop(this Rectangle rectangle, Point margin)
        {
            return new Rectangle(rectangle.X + margin.X, rectangle.Y + margin.Y, rectangle.Width - margin.X * 2, rectangle.Height - margin.Y * 2);
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