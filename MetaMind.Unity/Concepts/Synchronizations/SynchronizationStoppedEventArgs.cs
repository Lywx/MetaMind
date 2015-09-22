// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SynchronizationStoppedEventArgs.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Unity.Events
{
    using System;
    using Concepts.Synchronizations;

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