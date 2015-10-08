// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMMEngineInteropService.cs">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Components
{
    using Services;

    public interface IMMEngineInterop : IMMInputableComponent, IMMEngineInteropService
    {
        void OnExiting();
    }
}