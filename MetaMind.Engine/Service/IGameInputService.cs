// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IGameInputService.cs">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Service
{
    using Components.Input;

    public interface IGameInputService
    {
        IInputEvent Event { get; }

        IInputState State { get; }
    }
}