// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SynchronizationStartedEventArgs.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Perseverance.Events
{
    using System;

    using MetaMind.Perseverance.Concepts;

    public class SynchronizationStartedEventArgs : EventArgs
    {
        public readonly ISynchronizable Data;

        public SynchronizationStartedEventArgs(ISynchronizable data)
        {
            this.Data = data;
        }
    }
}