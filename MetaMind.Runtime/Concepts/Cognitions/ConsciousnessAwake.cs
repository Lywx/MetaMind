// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsciousnessAwake.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Runtime.Concepts.Cognitions
{
    using System;
    using System.Runtime.Serialization;

    using MetaMind.Engine.Components.Events;
    using MetaMind.Runtime.Events;
    using MetaMind.Runtime.Sessions;

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
                return this.Consciousness.HasEverSlept
                           ? DateTime.Now - this.Consciousness.LastSleepEndTime
                           : DateTime.Now - this.Consciousness.KnownOriginTime;
            }
        }

        public ConsciousnessAsleep Sleep(Consciousness consciousness)
        {
            consciousness.LastSleepStartTime = DateTime.Now;

            consciousness.KnownAwakeSpan += this.AwakeSpan;

            var console = this.GameInterop.Console;
            console.WriteLine(string.Format("MESSAGE: {0} in Awakening", this.AwakeSpan.ToString("hh':'mm':'ss''")));

            var @event = this.GameInterop.Event;
            @event.TriggerEvent(new Event((int)SessionEventType.SleepStarted, new ConsciousnessSleepStartedEventArgs(consciousness)));

            return new ConsciousnessAsleep(consciousness);
        }

        public override IConsciousnessState UpdateState(Consciousness consciousness)
        {
            return this;
        }
    }
}