// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IGameInputService.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Services
{
    using MetaMind.Engine.Components.Inputs;

    internal interface IGameInput : IGameInputableComponent
    {
        IInputState State { get; }

        IInputEvent Event { get; }
    }
}