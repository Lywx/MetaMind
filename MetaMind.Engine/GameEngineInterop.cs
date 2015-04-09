// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameEngineInterop.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine
{
    using MetaMind.Engine.Components;

    public sealed class GameEngineInterop : GameEngineAccess, IGameInterop
    {
        public GameEngineInterop()
        {
            this.AccessType = GameEngineAccessType.Interop;
        }

        public EventManager Event
        {
            get
            {
                return GameEngine.EventManager;
            }
        }

        public ProcessManager Process
        {
            get
            {
                return GameEngine.ProcessManager;
            }
        }
    }
}