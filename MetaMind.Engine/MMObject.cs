namespace MetaMind.Engine
{
    using System.Runtime.Serialization;
    using Components.Graphics;
    using Microsoft.Xna.Framework.Graphics;
    using Services;

    /// <summary>
    /// Common object for engine service access.
    /// </summary>
    [DataContract]
    public class MMObject
    {

        protected MMEngine GlobalEngine => this.GlobalInterop.Engine;

        #region Graphics

        protected IMMEngineGraphicsService GlobalGraphics => MMEngine.Service.Graphics;

        protected GraphicsDevice GlobalGraphicsDevice => this.GlobalGraphics.Device;

        protected IMMRenderer GlobalGraphicsRenderer => this.GlobalGraphics.Renderer;

        internal MMGraphicsDeviceController GlobalGraphicsDeviceController => this.GlobalGraphics.DeviceController;

        #endregion

        protected IMMEngineInteropService GlobalInterop => MMEngine.Service.Interop;

        protected IMMEngineInputService GlobalInput => MMEngine.Service.Input;

        protected IMMEngineNumericalService GlobalNumerical => MMEngine.Service.Numerical;
    }
}