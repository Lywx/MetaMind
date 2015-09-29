﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IGameInteropService.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Components
{
    using Service;

    public interface IGameInterop : IGameInputableComponent, IGameInteropService
    {
        void OnExiting();
    }
}