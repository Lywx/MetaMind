namespace MetaMind.Engine
{
    using System.Runtime.Serialization;
    using Services;

    /// <summary>
    /// Common object for engine service access.
    /// </summary>
    [DataContract]
    public class MMObject
    {
        #region Service

        protected MMEngine Engine => this.Interop.Engine;

        protected IMMEngineGraphicsService Graphics => MMEngine.Service.Graphics;

        protected IMMEngineInteropService Interop => MMEngine.Service.Interop;

        protected IMMEngineInputService Input => MMEngine.Service.Input;

        protected IMMEngineNumericalService Numerical => MMEngine.Service.Numerical;

        #endregion
    }
}