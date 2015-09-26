// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsciousnessAwake.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Unity.Concepts.Cognitions
{
    using System;
    using System.Runtime.Serialization;
    using Engine.Components.Interop.Event;
    using Events;
    using Sessions;

    [DataContract]
    internal class ConsciousnessAwake : ConsciousnessState, IConsciousnessAwake
    {
        public ConsciousnessAwake(Consciousness consciousness)
            : base(consciousness)
        {
        }

        public TimeSpan AwakeSpan
        {
            get
            {
                return this.Consciousness.HasSlept
                           ? DateTime.Now - this.Consciousness.LastSleepEndTime
                           : DateTime.Now - this.Consciousness.KnownOriginTime;
            }
        }

        public ConsciousnessAsleep Sleep(Consciousness consciousness)
        {
            consciousness.LastSleepStartTime = DateTime.Now;

            consciousness.KnownAwakeSpan += this.AwakeSpan;

            var console = this.Interop.Console;
            console.WriteLine($"MESSAGE: Awake {this.AwakeSpan.ToString("hh':'mm':'ss''")}");

            var @event = this.Interop.Event;
            @event.TriggerEvent(new Event((int)SessionEvent.SleepStarted, new ConsciousnessSleepStartedEventArgs(consciousness)));

            return new ConsciousnessAsleep(consciousness);
        }

        public override IConsciousnessState UpdateState(Consciousness consciousness)
        {
            return this;
        }
    }
}