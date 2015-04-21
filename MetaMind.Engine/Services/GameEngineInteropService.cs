// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameEngineInteropService.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Services
{
    using MetaMind.Engine.Components;

    /// <remarks>
    /// Sealed for added new keyword and changed acceesibility in GameEngine property
    /// </remarks>
    public sealed class GameEngineInteropService : GameEngineAccess, IGameInteropService
    {
        public GameEngineInteropService(GameEngine engine)
            : base(engine)
        {
        }

        public IEventManager Event
        {
            get
            {
                return this.Engine.Event;
            }
        }

        public IGameManager Game
        {
            get
            {
                return this.Engine.Games;
            }
        }

        public new GameEngine Engine
        {
            get
            {
                return this.Engine;
            }
        }

        public IProcessManager Process
        {
            get
            {
                return this.Engine.Process;
            }
        }

        public IScreenManager Screen
        {
            get
            {
                return this.Engine.Screen;
            }
        }
    }
}