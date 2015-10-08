// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsciousnessSleepStartedEventArgs.cs">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Session.Concepts.Cognitions
{
    using System;

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