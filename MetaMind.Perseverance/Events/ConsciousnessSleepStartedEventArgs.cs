﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsciousnessSleepStartedEventArgs.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Perseverance.Concepts.Cognitions
{
    using System;

    public class ConsciousnessSleepStartedEventArgs : EventArgs
    {
        public readonly ConsciousnessAwake State;

        public ConsciousnessSleepStartedEventArgs(ConsciousnessAwake state)
        {
            this.State = state;
        }
    }
}