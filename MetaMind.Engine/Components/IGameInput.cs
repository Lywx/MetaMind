﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IGameInput.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Components
{
    using MetaMind.Engine.Services;

    public interface IGameInput : IGameControllableComponent, IGameInputService
    {
    }
}