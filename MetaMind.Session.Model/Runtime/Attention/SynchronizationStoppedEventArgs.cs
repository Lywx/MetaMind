// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SynchronizationStoppedEventArgs.cs">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Session.Model.Runtime.Attention
{
    using System;
    using Process;

    public class SynchronizationStoppedEventArgs : EventArgs
    {
        private readonly IJobSynchronizationData data;

        public SynchronizationStoppedEventArgs(IJobSynchronizationData data)
        {
            this.data = data;
        }

        public IJobSynchronizationData Data
        {
            get
            {
                return this.data;
            }
        }
    }
}