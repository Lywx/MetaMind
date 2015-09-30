// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MMEngineInputService.cs">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Service
{
    using System;
    using Components;
    using Components.Input;

    public sealed class MMEngineInputService : IMMEngineInputService
    {
        private readonly IMMEngineInput input;

        public MMEngineInputService(IMMEngineInput input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            this.input = input;
        }

        public IInputEvent Event => this.input.Event;

        public IInputState State => this.input.State;
    }
}