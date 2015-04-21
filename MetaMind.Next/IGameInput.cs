// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IGameInputService.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Next
{
    using MetaMind.Engine.Components.Inputs;

    using Microsoft.Xna.Framework;

    internal interface IGameInput : IGameComponent
    {
        IInputState State { get; }

        IInputEvent Event { get; }
    }
}