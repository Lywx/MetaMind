// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SynchronizationDataProcessor.cs" company="UESTC">
//   Copyright (c) 2014 Lin Wuxiang
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Perseverance.Concepts.Cognitions
{
    using System;
    using System.Runtime.Serialization;

    using MetaMind.Engine.Concepts;
    using MetaMind.Perseverance.Concepts.TaskEntries;

    [DataContract]
    public class SynchronizationDataProcessor
    {
        [DataMember(Name = "Target")]
        public TaskEntry Target { get; private set; }

        public void Accept(TaskEntry task)
        {
            this.Target = task;
            this.Target.Synchronizing = true;
            this.Target.Experience += new Experience();
        }

        public void Release(out TimeSpan timePassed)
        {
            timePassed = this.Target.Experience.End();

            this.Target.Synchronizing = false;
            this.Target = null;
        }

        public void Abort()
        {
            this.Target.Experience.Abort();
            this.Target.Synchronizing = false;
            this.Target = null;
        }

        public void Update(bool enabled, double acceleration)
        {
            if (enabled)
            {
                this.Target.Experience.Accelaration = acceleration;
            }
        }
    }
}