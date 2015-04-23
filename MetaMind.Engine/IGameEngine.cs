﻿namespace MetaMind.Engine
{
    using MetaMind.Engine.Components;

    public interface IGameEngine
    {
        #region Components

        IGameInput Input { get; }

        IGameInterop Interop { get; }

        IGameGraphics Graphics { get; }

        #endregion

        #region Operations

        void Run();

        void Restart();

        #endregion
    }
}