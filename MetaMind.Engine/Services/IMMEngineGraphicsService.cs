namespace MetaMind.Engine.Services
{
    using Components.Graphics;
    using Microsoft.Xna.Framework.Graphics;

    public interface IMMEngineGraphicsService
    {
        #region Manager and Settings

        MMGraphicsManager Manager { get; }

        MMGraphicsSettings Settings { get; }

        #endregion

        #region Renderer

        IMMRenderer Renderer { get; }

        MMRenderDeviceController DeviceController { get; }

        GraphicsDevice Device { get; }

        #endregion
    }
}