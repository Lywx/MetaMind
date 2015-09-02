// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IGameInputService.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Services
{
    using Components.Inputs;

    public interface IGameInputService
    {
        IInputEvent Event { get; }

        IInputState State { get; }
    }
}