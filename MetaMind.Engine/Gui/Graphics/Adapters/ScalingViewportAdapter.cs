namespace MetaMind.Engine.Gui.Graphics.Adapters
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// Scaling adapter for certain virtual entity (like texture and logo) to scale according to the viewport.
    /// </summary>
    public class ScalingViewportAdapter : ViewportAdapter
    {
        public ScalingViewportAdapter(GraphicsDevice graphicsDevice, int virtualWidth, int virtualHeight) 
        {
            if (graphicsDevice == null)
            {
                throw new ArgumentNullException(nameof(graphicsDevice));
            }

            this.GraphicsDevice = graphicsDevice;
            this.VirtualWidth   = virtualWidth;
            this.VirtualHeight  = virtualHeight;
        }

        protected GraphicsDevice GraphicsDevice { get; private set; }

        public override int VirtualWidth { get; }

        public override int VirtualHeight { get; }

        public override int ViewportWidth => this.GraphicsDevice.Viewport.Width;

        public override int ViewportHeight => this.GraphicsDevice.Viewport.Height;

        public override void OnClientSizeChanged()
        {
        }

        public override Matrix GetScaleMatrix()
        {
            var scaleX = (float)this.ViewportWidth / this.VirtualWidth;
            var scaleY = (float)this.ViewportHeight / this.VirtualHeight;
            return Matrix.CreateScale(scaleX, scaleY, 1.0f);
        }
    }
}