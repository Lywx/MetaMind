// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IGameInteropService.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Services
{
    using MetaMind.Engine;
    using MetaMind.Engine.Components;

    using Microsoft.Xna.Framework;

    internal interface IGameInterop : IGameComponent
    {
        IEventManager Event { get; }

        IGameManager Game { get; }

        IGameEngine Engine { get; }

        IProcessManager Process { get; }

        IScreenManager Screen { get; }
    }
}