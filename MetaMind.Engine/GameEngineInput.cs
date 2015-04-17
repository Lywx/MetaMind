// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameEngineInput.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine
{
    using MetaMind.Engine.Components.Inputs;

    public sealed class GameEngineInput : GameEngineAccess, IGameInput
    {
        public GameEngineInput(GameEngine gameEngine)
            : base(gameEngine)
        {
            this.AccessType = GameEngineAccessType.Input;
        }

        public IInputEvent Event
        {
            get
            {
                return GameEngine.InputEvent;
            }
        }

        public IInputState State
        {
            get
            {
                return GameEngine.InputState;
            }
        }
    }
}