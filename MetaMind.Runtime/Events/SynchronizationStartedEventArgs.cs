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
        private readonly ISynchronizationData data;

        public SynchronizationStartedEventArgs(ISynchronizationData data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

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