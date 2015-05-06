// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsciousnessSleepStoppedEventArgs.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Testimony.Events
{
    using MetaMind.Testimony.Concepts.Cognitions;

    public class ConsciousnessSleepStoppedEventArgs
    {
        private readonly Consciousness consciousness;

        public ConsciousnessSleepStoppedEventArgs(Consciousness consciousness)
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