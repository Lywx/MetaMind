namespace MetaMind.Engine.Components.Graphics.Adapters
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class DefaultViewportAdapter : ViewportAdapter
    {
        public DefaultViewportAdapter(GraphicsDevice graphicsDevice)
        {
            this.Device = graphicsDevice;
        }

        public GraphicsDevice Device { get; }

        public override int VirtualWidth => this.Device.Viewport.Width;

        public override int VirtualHeight => this.Device.Viewport.Height;

        public override int ViewportWidth => this.Device.Viewport.Width;

        public override int ViewportHeight => this.Device.Viewport.Height;

        public override void OnClientSizeChanged()
        {
        }

        public override Matrix GetScaleMatrix()
        {
            return Matrix.Identity;
        }
    }
}