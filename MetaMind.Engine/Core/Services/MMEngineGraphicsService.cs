namespace MetaMind.Engine.Core.Services
{
    using Backend;
    using Backend.Graphics;
    using Microsoft.Xna.Framework.Graphics;

    public sealed class MMEngineGraphicsService : IMMEngineGraphicsService
    {
        public MMEngineGraphicsService(IMMEngineGraphics graphics)
        {
            this.Graphics = graphics;
        }

        private IMMEngineGraphics Graphics { get; }


        #region Manager and Settings

        public MMGraphicsManager Manager => this.Graphics.Manager;

        public MMGraphicsSettings Settings => this.Graphics.Settings;

        #endregion

        #region Renderer

        public GraphicsDevice Device => this.Manager.GraphicsDevice;

        public IMMRenderer Renderer => this.Graphics.Renderer;

        public MMGraphicsDeviceController DeviceController => this.Graphics.DeviceController;

        #endregion
    }
}