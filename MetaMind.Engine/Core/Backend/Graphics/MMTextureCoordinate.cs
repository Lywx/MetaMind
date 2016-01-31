namespace MetaMind.Engine.Core.Backend.Graphics
{
    using Microsoft.Xna.Framework;

    public struct MMTextureCoordinate
    {
        public Vector2 BottomRight;

        public Vector2 TopLeft;

        public MMTextureCoordinate(Vector2 topLeft, Vector2 bottomRight)
        {
            this.TopLeft     = topLeft;
            this.BottomRight = bottomRight;
        }
    }
}
