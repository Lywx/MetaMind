// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SynchronizationStartedEventArgs.cs" company="UESTC">
//   Copyright (c) 2014 Lin Wuxiang
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Perseverance.Concepts.Cognitions
{
    using System;

    using MetaMind.Perseverance.Concepts.TaskEntries;

    public class SynchronizationStartedEventArgs : EventArgs
    {
        public readonly TaskEntry TaskEntry;

        public SynchronizationStartedEventArgs(TaskEntry data)
        {
            TaskEntry = data;
        }
    }
}