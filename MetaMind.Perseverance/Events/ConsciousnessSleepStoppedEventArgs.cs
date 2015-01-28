// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsciousnessSleepStoppedEventArgs.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Perseverance.Concepts.Cognitions
{
    public class ConsciousnessSleepStoppedEventArgs
    {
        public readonly ConsciousnessSleepy State;

        public ConsciousnessSleepStoppedEventArgs(ConsciousnessSleepy state)
        {
            this.State = state;
        }
    }
}