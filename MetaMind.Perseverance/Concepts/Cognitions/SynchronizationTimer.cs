// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SynchronizationTimer.cs" company="UESTC">
//   Copyright (c) 2014 Lin Wuxiang
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Perseverance.Concepts.Cognitions
{
    using System;
    using System.Runtime.Serialization;

    using Microsoft.Xna.Framework;

    [DataContract]
    public class SynchronizationTimer
    {
        [DataMember]
        private DateTime currentMoment = DateTime.Now;

        [DataMember]
        private bool enabled;

        [DataMember]
        private DateTime previousMoment;

        [DataMember]
        private DateTime transitionMoment;

        public SynchronizationTimer()
        {
            this.AccumulatedTime = TimeSpan.Zero;
        }
        
        [DataMember]
        public TimeSpan AccumulatedTime { get; set; }

        public TimeSpan ElapsedTimeSinceTransition
        {
            get { return this.transitionMoment.Ticks != 0 ? this.currentMoment - this.transitionMoment : TimeSpan.Zero; }
        }

        public bool Enabled
        {
            get { return this.enabled; }
        }

        private TimeSpan ElapsedTime
        {
            get { return this.currentMoment - this.previousMoment; }
        }

        public void Start()
        {
            this.Transition();
        }

        public void Stop()
        {
            this.Transition();
        }

        public void Update(GameTime gameTime)
        {
            // record time regardless whether update get called
            this.previousMoment = this.currentMoment;
            this.currentMoment = DateTime.Now;

            if (this.Enabled)
            {
                this.AccumulatedTime += this.ElapsedTime;
            }
            else
            {
                this.AccumulatedTime -= this.ElapsedTime;
                if (this.AccumulatedTime.TotalSeconds < 0)
                {
                    this.AccumulatedTime = TimeSpan.Zero;
                }
            }
        }

        private void Transition()
        {
            this.enabled = !this.enabled;
            this.transitionMoment = DateTime.Now;
        }
    }
}