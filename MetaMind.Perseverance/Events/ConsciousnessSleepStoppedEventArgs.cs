﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsciousnessSleepStoppedEventArgs.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Perseverance.Events
{
    using MetaMind.Perseverance.Concepts.Cognitions;

    public class ConsciousnessSleepStoppedEventArgs
    {
        public readonly ConsciousnessAsleep State;

        public ConsciousnessSleepStoppedEventArgs(ConsciousnessAsleep state)
        {
            this.State = state;
        }
    }
}