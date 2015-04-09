// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SynchronizationStoppedEventArgs.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Perseverance.Events
{
    using System;

    using MetaMind.Perseverance.Concepts.Tasks;

    public class SynchronizationStoppedEventArgs : EventArgs
    {
        public readonly Task Task;

        public SynchronizationStoppedEventArgs(Task data)
        {
            this.Task = data;
        }
    }
}