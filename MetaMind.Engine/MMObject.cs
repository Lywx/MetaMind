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

        protected MMEngine Engine => this.Interop.Engine;

        #region Graphics

        protected IMMEngineGraphicsService Graphics => MMEngine.Service.Graphics;

        protected GraphicsDevice GraphicsDevice => this.Graphics.Device;

        protected IMMRenderer GraphicsRenderer => this.Graphics.Renderer;

        internal MMRenderDeviceController GraphicsDeviceController => this.Graphics.DeviceController;

        #endregion

        protected IMMEngineInteropService Interop => MMEngine.Service.Interop;

        protected IMMEngineInputService Input => MMEngine.Service.Input;

        protected IMMEngineNumericalService Numerical => MMEngine.Service.Numerical;
    }
}