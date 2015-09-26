namespace MetaMind.Engine.Components.Graphics.Adapters
{
    using Microsoft.Xna.Framework;

    public abstract class ViewportAdapter
    {
        protected ViewportAdapter()
        {
        }

        public abstract int VirtualWidth { get; }

        public abstract int VirtualHeight { get; }

        public abstract int ViewportWidth { get; }

        public abstract int ViewportHeight { get; }

        public abstract void OnClientSizeChanged();

        public abstract Matrix GetScaleMatrix();

        public Point PointToScreen(Point point)
        {
            return this.PointToScreen(point.X, point.Y);
        }

        public virtual Point PointToScreen(int x, int y)
        {
            var scaleMatrix = this.GetScaleMatrix();
            var invertedMatrix = Matrix.Invert(scaleMatrix);
            return Vector2.Transform(new Vector2(x, y), invertedMatrix).ToPoint();
        }
    }
}