﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsciousnessSleepStartedEventArgs.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Runtime.Events
{
    using System;

    using MetaMind.Runtime.Concepts.Cognitions;

    public class ConsciousnessSleepStartedEventArgs : EventArgs
    {
        private readonly Consciousness consciousness;

        public ConsciousnessSleepStartedEventArgs(Consciousness consciousness)
        {
            this.consciousness = consciousness;
        }

        public Consciousness Consciousness
        {
            get
            {
                return this.consciousness;
            }
        }
    }
}