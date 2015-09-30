// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMMEngineInputService.cs">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Service
{
    using Components.Input;

    public interface IMMEngineInputService
    {
        IInputEvent Event { get; }

        IInputState State { get; }
    }
}