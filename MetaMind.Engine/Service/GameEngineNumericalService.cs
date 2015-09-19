// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameEngineNumericalService.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Service
{
    using System;

    public class GameEngineNumericalService : IGameNumericalService
    {
        private readonly IGameNumerical numerical;

        public GameEngineNumericalService(IGameNumerical numerical)
        {
            if (numerical == null)
            {
                throw new ArgumentNullException(nameof(numerical));
            }

            this.numerical = numerical;
        }

        public Random Random => this.numerical.Random;
    }
}