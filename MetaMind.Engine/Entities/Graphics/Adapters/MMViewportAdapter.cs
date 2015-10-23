namespace MetaMind.Engine.Entities.Graphics.Adapters
{
    using Microsoft.Xna.Framework;

    public abstract class MMViewportAdapter : MMObject
    {
        #region Constructors and Finalizer

        protected MMViewportAdapter()
        {
        }

        #endregion

        #region Adapter Data

        public abstract int VirtualWidth { get; }

        public abstract int VirtualHeight { get; }

        public abstract int ViewportWidth { get; }

        public abstract int ViewportHeight { get; }

        #endregion

        #region Adapter Operations

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

        #endregion

        #region Event Ons

        public abstract void OnClientSizeChanged();

        #endregion
    }
}