namespace MetaMind.Engine.Entities.Graphics.Adapters
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// Scaling adapter for certain virtual entity (like texture and logo) to
    /// scale according to the viewport.
    /// </summary>
    public class MMScalingViewportAdapter : MMViewportAdapter
    {
        #region Constructors and Finalizer

        public MMScalingViewportAdapter(
            GraphicsDevice device,
            int virtualWidth,
            int virtualHeight) 
        {
            this.VirtualWidth   = virtualWidth;
            this.VirtualHeight  = virtualHeight;
        }

        #endregion

        #region Adapter Data

        public override int VirtualWidth { get; }

        public override int VirtualHeight { get; }

        public override int ViewportWidth => this.GlobalGraphicsDevice.Viewport.Width;

        public override int ViewportHeight => this.GlobalGraphicsDevice.Viewport.Height;

        #endregion

        #region Event Ons

        public override void OnClientSizeChanged()
        {
        }

        #endregion

        #region Adapter Operations

        public override Matrix GetScaleMatrix()
        {
            var xScale = (float)this.ViewportWidth / this.VirtualWidth;
            var yScale = (float)this.ViewportHeight / this.VirtualHeight;
            return Matrix.CreateScale(xScale, yScale, 1.0f);
        }

        #endregion
    }
}