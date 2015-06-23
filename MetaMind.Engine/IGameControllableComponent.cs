// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IGameControllableComponent.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine
{
    using Microsoft.Xna.Framework;

    public interface IGameControllableComponent : IGameComponent, IGameControllableComponentOperations 
    {
        bool Controllable { get; }
    }
}