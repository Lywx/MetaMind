// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SynchronizationStoppedEventArgs.cs">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Session.Concepts.Synchronizations
{
    using System;

    public class SynchronizationStoppedEventArgs : EventArgs
    {
        private readonly ISynchronizationData data;

        public SynchronizationStoppedEventArgs(ISynchronizationData data)
        {
            this.data = data;
        }

        public ISynchronizationData Data
        {
            get
            {
                return this.data;
            }
        }
    }
}