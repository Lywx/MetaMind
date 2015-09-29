// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameObject.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
namespace MetaMind.Engine
{
    using System.Runtime.Serialization;
    using Service;

    [DataContract]
    public class GameObject
    {
        #region Dependency

        protected GameEngine Engine => this.Interop.Engine;

        protected IGameGraphicsService Graphics => GameEngine.Service.Graphics;

        protected IGameInteropService Interop => GameEngine.Service.Interop;

        protected IGameInputService Input => GameEngine.Service.Input;

        protected IGameNumericalService Numerical => GameEngine.Service.Numerical;

        #endregion
    }
}