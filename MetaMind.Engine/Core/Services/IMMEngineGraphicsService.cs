namespace MetaMind.Engine.Core.Services
{
    using Backend.Graphics;
    using Microsoft.Xna.Framework.Graphics;

    public interface IMMEngineGraphicsService
    {
        #region Manager and Settings

        MMGraphicsManager Manager { get; }

        MMGraphicsSettings Settings { get; }

        #endregion

        #region Renderer

        IMMRenderer Renderer { get; }

        MMGraphicsDeviceController DeviceController { get; }

        GraphicsDevice Device { get; }

        #endregion
    }
}