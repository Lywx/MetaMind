// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IGameInputService.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Service
{
    using Component.Input;

    public interface IGameInputService
    {
        IInputEvent Event { get; }

        IInputState State { get; }
    }
}