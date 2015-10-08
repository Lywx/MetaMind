// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SynchronizationProcessor.cs">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Session.Concepts.Synchronizations
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    [KnownType(typeof(SynchronizationData))]
    public class SynchronizationProcessor
    {
        [DataMember]
        public ISynchronizationData Data { get; set; }

        public void Accept(ISynchronizationData data)
        {
            this.Data = data;

            this.Data.SynchronizationSpan += new SynchronizationSpan();
            this.Data.IsSynchronizing = true;
        }

        public void Release(out TimeSpan timePassed)
        {
            timePassed = this.Data.SynchronizationSpan.End();

            this.Data.IsSynchronizing = false;
            this.Data = null;
        }

        public void Abort()
        {
            this.Data.SynchronizationSpan.Abort();

            this.Data.IsSynchronizing = false;
            this.Data = null;
        }

        public void Update(bool isEnabled, double acceleration)
        {
            if (isEnabled)
            {
                this.Data.SynchronizationSpan.Accelaration = acceleration;
            }
        }
    }
}