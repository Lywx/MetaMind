// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MMEngineNumerical.cs">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Core.Backend
{
    using System;
    using Services;

    public class MMEngineNumerical : IMMEngineNumerical
    {
        public MMEngineNumerical()
        {
            this.Random = new Random((int)DateTime.Now.Ticks);
        }

        public Random Random { get; private set; }

        public void Initialize()
        {
        }
    }
}