// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SynchronizationStartedEventArgs.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Runtime.Events
{
    using System;

    using MetaMind.Runtime.Concepts.Synchronizations;

    public class SynchronizationStartedEventArgs : EventArgs
    {
        public readonly ISynchronizable Data;

        public SynchronizationStartedEventArgs(ISynchronizable data)
        {
            this.Data = data;
        }
    }
}