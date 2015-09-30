// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IGameControllableComponent.cs">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine
{
    using System;

    public interface IGameInputableComponent : IDrawableGameComponent, IGameInputableComponentOperations, IDisposable
    {
    }
}