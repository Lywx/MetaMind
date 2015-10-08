// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SynchronizationStartedEventArgs.cs">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Session.Concepts.Synchronizations
{
    using System;

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