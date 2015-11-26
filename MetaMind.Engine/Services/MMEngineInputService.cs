// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MMEngineInputService.cs">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Services
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

        public IMMInputEvent Event => this.input.Event;

        public IMMInputState State => this.input.State;
    }
}