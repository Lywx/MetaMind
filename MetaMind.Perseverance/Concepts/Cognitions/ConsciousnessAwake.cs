// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsciousnessAwake.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Perseverance.Concepts.Cognitions
{
    using System;
    using System.Runtime.Serialization;

    using MetaMind.Engine.Components.Events;
    using MetaMind.Perseverance.Events;
    using MetaMind.Perseverance.Sessions;

    [DataContract]
    public class ConsciousnessAwake : Consciousness
    {
        public ConsciousnessAwake()
        {
        }

        public ConsciousnessAwake(ConsciousnessSleepy state)
        {
            this.SleepEndTime        = state.SleepEndTime;
            this.SleepStartTime      = state.SleepStartTime;
            this.HistoricalAwakeSpan = state.HistoricalAwakeSpan;
            this.HistoricalSleepSpan = state.HistoricalSleepSpan;
        }

        public TimeSpan AwakeSpan
        {
            get
            {
                return DateTime.Now - this.SleepEndTime;
            }
        }

        public void AwakeNow()
        {
            this.SleepEndTime = DateTime.Now;
        }

        public ConsciousnessSleepy Sleep()
        {
            this.AwakeNow();

            if (this.HasEverSlept)
            {
                var totalAwakeSpan = this.SleepStartTime - this.SleepEndTime;
                this.HistoricalAwakeSpan += totalAwakeSpan;

                GameGraphics.MessageDrawer.PopMessages("Awake for " + totalAwakeSpan.ToString("hh':'mm':'ss''"));
            }

            this.Interop.Event.TriggerEvent(new Event((int)SessionEventType.SleepStarted, new ConsciousnessSleepStartedEventArgs(this)));

            return new ConsciousnessSleepy(this);
        }
    }
}