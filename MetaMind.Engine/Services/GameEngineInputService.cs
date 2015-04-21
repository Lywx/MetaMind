// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameEngineInputService.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Services
{
    using System;

    using MetaMind.Engine.Components;
    using MetaMind.Engine.Components.Inputs;

    public sealed class GameEngineInputService : IGameInputService
    {
        private readonly IGameInput input;

        public GameEngineInputService(IGameInput input)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }

            this.input = input;
        }

        public IInputEvent Event
        {
            get
            {
                return this.input.Event;
            }
        }

        public IInputState State
        {
            get
            {
                return this.input.State;
            }
        }
    }
}