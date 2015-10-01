namespace MetaMind.Engine
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Service;

    public static class MMRenderTargetFactory
    {
        private static readonly RenderTargetUsage RenderTargetUsage = RenderTargetUsage.DiscardContents;

        private static IMMEngineGraphicsService Graphics => MMEngine.Service.Graphics;

        private static GraphicsDevice GraphicsDevice => Graphics.GraphicsDevice;

        public static RenderTarget2D Create(Point size)
        {
            return Create(size.X, size.Y);
        }

        public static RenderTarget2D Create(int width, int height)
        {
            if (width <= 0 || 
                height <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            return new RenderTarget2D(
                GraphicsDevice,
                width,
                height,
                false,
                SurfaceFormat.Color,
                DepthFormat.None,
                GraphicsDevice.PresentationParameters.MultiSampleCount,
                RenderTargetUsage);
        }
    }
}