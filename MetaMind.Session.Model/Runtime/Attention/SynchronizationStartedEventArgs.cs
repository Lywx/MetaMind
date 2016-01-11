// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SynchronizationStartedEventArgs.cs">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Session.Model.Runtime.Attention
{
    using System;
    using Process;

    public class SynchronizationStartedEventArgs : EventArgs
    {
        private readonly IJobSynchronizationData data;

        public SynchronizationStartedEventArgs(IJobSynchronizationData data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

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