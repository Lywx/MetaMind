// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameEngineService.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Services
{
    using System;

    public class GameEngineService : IGameService
    {
        public GameEngineService(
            IGameGraphicsService graphics,
            IGameInputService input,
            IGameInteropService interop,
            IGameNumericalService numerical)
        {
            if (graphics == null)
            {
                throw new ArgumentNullException(nameof(graphics));
            }

            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (interop == null)
            {
                throw new ArgumentNullException(nameof(interop));
            }

            if (numerical == null)
            {
                throw new ArgumentNullException(nameof(numerical));
            }

            this.Graphics = graphics;
            this.Input = input;
            this.Interop = interop;
            this.Numerical = numerical;
        }

        public IGameGraphicsService Graphics { get; }

        public IGameInputService Input { get; }

        public IGameInteropService Interop { get; }

        public IGameNumericalService Numerical { get; }
    }
}