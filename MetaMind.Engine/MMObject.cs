// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MMObject.cs">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
namespace MetaMind.Engine
{
    using System.Runtime.Serialization;
    using Gui.Graphics.Adapters;
    using Microsoft.Xna.Framework.Graphics;
    using Service;

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

        #region Graphics

        protected GraphicsDevice GraphicsDevice => this.Graphics.GraphicsDevice;

        protected SpriteBatch SpriteBatch => this.Graphics.SpriteBatch;

        protected Viewport Viewport => this.GraphicsDevice.Viewport;

        protected ViewportAdapter ViewportAdapter { get; set; }

        #endregion
    }
}