// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SynchronizationStoppedEventArgs.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Perseverance.Concepts.Cognitions
{
    using System;

    using MetaMind.Perseverance.Concepts.TaskEntries;

    public class SynchronizationStoppedEventArgs : EventArgs
    {
        public readonly TaskEntry TaskEntry;

        public SynchronizationStoppedEventArgs(TaskEntry data)
        {
            TaskEntry = data;
        }
    }
}