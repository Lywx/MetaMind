// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameEngineInput.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine
{
    using MetaMind.Engine.Components;

    public sealed class GameEngineInput : GameEngineAccess, IGameInput
    {
        public GameEngineInput()
        {
            this.AccessType = GameEngineAccessType.Input;
        }

        public InputEventManager Event
        {
            get
            {
                return GameEngine.InputEventManager;
            }
        }

        public InputSequenceManager Sequence
        {
            get
            {
                return GameEngine.InputSequenceManager;
            }
        }
    }
}