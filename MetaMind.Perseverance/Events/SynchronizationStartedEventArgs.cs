// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SynchronizationStartedEventArgs.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Perseverance.Events
{
    using System;

    using MetaMind.Perseverance.Concepts.Tasks;

    public class SynchronizationStartedEventArgs : EventArgs
    {
        public readonly Task Task;

        public SynchronizationStartedEventArgs(Task data)
        {
            this.Task = data;
        }
    }
}