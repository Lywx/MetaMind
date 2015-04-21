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
        private readonly IGameGraphicsService graphics;

        private readonly IGameInputService input;

        private readonly IGameInteropService interop;

        private readonly IGameNumericalService numerical;

        public GameEngineService(
            IGameGraphicsService  graphics,
            IGameInputService     input,
            IGameInteropService   interop,
            IGameNumericalService numerical)
        {
            if (graphics == null)
            {
                throw new ArgumentNullException("graphics");
            }

            if (input == null)
            {
                throw new ArgumentNullException("input");
            }

            if (interop == null)
            {
                throw new ArgumentNullException("interop");
            }

            if (numerical == null)
            {
                throw new ArgumentNullException("numerical");
            }

            this.graphics  = graphics;
            this.input     = input;
            this.interop   = interop;
            this.numerical = numerical;
        }

        public IGameGraphicsService Graphics
        {
            get
            {
                return this.graphics;
            }
        }

        public IGameInputService Input
        {
            get
            {
                return this.input;
            }
        }

        public IGameInteropService Interop
        {
            get
            {
                return this.interop;
            }
        }

        public IGameNumericalService Numerical
        {
            get
            {
                return this.numerical;
            }
        }
    }
}