// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IGameInteropService.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Component
{
    using Service;

    public interface IGameInterop : IGameControllableComponent, IGameInteropService
    {
        void OnExiting();
    }
}