// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameObject.cs">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
namespace MetaMind.Engine
{
    using System.Runtime.Serialization;
    using Microsoft.Xna.Framework.Graphics;
    using Service;
    using Components.Graphics.Adapters;

    [DataContract]
    public class GameObject
    {
        #region Service

        protected GameEngine Engine => this.Interop.Engine;

        protected IGameGraphicsService Graphics => GameEngine.Service.Graphics;

        protected IGameInteropService Interop => GameEngine.Service.Interop;

        protected IGameInputService Input => GameEngine.Service.Input;

        protected IGameNumericalService Numerical => GameEngine.Service.Numerical;

        #endregion

        #region Graphics

        protected GraphicsDevice GraphicsDevice => this.Graphics.GraphicsDevice;

        protected SpriteBatch SpriteBatch => this.Graphics.SpriteBatch;

        protected Viewport Viewport => this.GraphicsDevice.Viewport;

        protected ViewportAdapter ViewportAdapter { get; set; }

        #endregion
    }
}