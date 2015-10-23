namespace MetaMind.Engine.Entities.Graphics.Adapters
{
    using Microsoft.Xna.Framework;

    public class MMDefaultViewportAdapter : MMViewportAdapter
    {
        #region Constructors and Finalizer

        public MMDefaultViewportAdapter()
        {
        }

        #endregion

        public override int VirtualWidth => this.GraphicsDevice.Viewport.Width;

        public override int VirtualHeight => this.GraphicsDevice.Viewport.Height;

        public override int ViewportWidth => this.GraphicsDevice.Viewport.Width;

        public override int ViewportHeight => this.GraphicsDevice.Viewport.Height;

        public override void OnClientSizeChanged()
        {
        }

        public override Matrix GetScaleMatrix()
        {
            return Matrix.Identity;
        }
    }
}