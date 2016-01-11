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

        public override int VirtualWidth => this.GlobalGraphicsDevice.Viewport.Width;

        public override int VirtualHeight => this.GlobalGraphicsDevice.Viewport.Height;

        public override int ViewportWidth => this.GlobalGraphicsDevice.Viewport.Width;

        public override int ViewportHeight => this.GlobalGraphicsDevice.Viewport.Height;

        public override void OnClientSizeChanged()
        {
        }

        public override Matrix GetScaleMatrix()
        {
            return Matrix.Identity;
        }
    }
}