// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SynchronizationProcessor.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Perseverance.Concepts
{
    using System;
    using System.Runtime.Serialization;

    using MetaMind.Engine.Concepts;

    [DataContract]
    public class SynchronizationProcessor
    {
        [DataMember]
        public ISynchronizable Data { get; private set; }

        public SynchronizationData DataSynchronization
        {
            get { return this.Data.SynchronizationData; }
        }

        public void Accept(ISynchronizable data)
        {
            this.Data = data;

            this.DataSynchronization.SynchronizationSpan += new SynchronizationSpan();
            this.DataSynchronization.IsSynchronizing = true;
        }

        public void Release(out TimeSpan timePassed)
        {
            timePassed = this.Data.SynchronizationData.SynchronizationSpan.End();

            this.DataSynchronization.IsSynchronizing = false;
            this.Data = null;
        }

        public void Abort()
        {
            this.DataSynchronization.SynchronizationSpan.Abort();

            this.DataSynchronization.IsSynchronizing = false;

            this.Data = null;
        }

        public void Update(bool isEnabled, double acceleration)
        {
            if (isEnabled)
            {
                this.DataSynchronization.SynchronizationSpan.Accelaration = acceleration;
            }
        }
    }
}