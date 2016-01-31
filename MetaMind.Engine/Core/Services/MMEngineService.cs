// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MMEngineService.cs">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Core.Services
{
    using System;

    public class MMEngineService : IMMEngineService
    {
        public MMEngineService(
            IMMEngineGraphicsService graphics,
            IMMEngineInputService input,
            IMMEngineInteropService interop,
            IMMEngineNumericalService numerical)
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

        public IMMEngineGraphicsService Graphics { get; }

        public IMMEngineInputService Input { get; }

        public IMMEngineInteropService Interop { get; }

        public IMMEngineNumericalService Numerical { get; }
    }
}