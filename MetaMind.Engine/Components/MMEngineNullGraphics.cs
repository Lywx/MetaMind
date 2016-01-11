namespace MetaMind.Engine.Components
{
    using Graphics;
    using Interop;
    using Microsoft.Xna.Framework.Graphics;

    internal class MMEngineNullGraphics : IMMEngineGraphics
    {
        public MMGraphicsManager Manager { get; }

        public IMMScreenDirector Screen { get; }

        public MMGraphicsSettings Settings { get; }

        public SpriteBatch SpriteBatch { get; }

        public IMMRenderer Renderer { get; }

        public MMGraphicsDeviceController DeviceController { get; }

        public GraphicsDevice Device { get; }

        public void Initialize()
        {
        }

        public void Dispose()
        {
        }
    }
}