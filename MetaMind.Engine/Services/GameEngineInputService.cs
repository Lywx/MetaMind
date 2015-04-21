// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameEngineInputService.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Services
{
    using MetaMind.Engine.Components.Inputs;

    public sealed class GameEngineInputService : GameEngineAccess, IGameInputService
    {
        public GameEngineInputService(GameEngine engine)
            : base(engine)
        {
            this.AccessType = GameEngineAccessType.Input;
        }

        public IInputEvent Event
        {
            get
            {
                return this.Engine.InputEvent;
            }
        }

        public IInputState State
        {
            get
            {
                return this.Engine.InputState;
            }
        }
    }
}