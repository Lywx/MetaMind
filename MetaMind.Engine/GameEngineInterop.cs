// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameEngineInterop.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine
{
    using MetaMind.Engine.Components;

    /// <remarks>
    /// Sealed for added new keyword and changed acceesibility in GameEngine property
    /// </remarks>
    public sealed class GameEngineInterop : GameEngineAccess, IGameInterop
    {
        public GameEngineInterop(GameEngine gameEngine)
            : base(gameEngine)
        {
            this.AccessType = GameEngineAccessType.Interop;
        }

        public GameEngineInterop(IGameInterop gameInterop)
            : this(gameInterop.GameEngine)
        {
        }

        public IEventManager Event
        {
            get
            {
                return GameEngine.EventManager;
            }
        }

        public IGameManager Game
        {
            get
            {
                return this.GameEngine.GameManager;
            }
        }

        public new GameEngine GameEngine
        {
            get
            {
                return this.GameEngine;
            }
        }

        public IProcessManager Process
        {
            get
            {
                return GameEngine.ProcessManager;
            }
        }

        public IScreenManager Screen
        {
            get
            {
                return this.GameEngine.ScreenManager;
            }
        }
    }
}