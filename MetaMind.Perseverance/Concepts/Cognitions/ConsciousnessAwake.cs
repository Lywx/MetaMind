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

    using Microsoft.Xna.Framework;

    [DataContract]
    internal class ConsciousnessAwake : ConsciousnessState
    {
        public ConsciousnessAwake(Consciousness consciousness)
            : base(consciousness)
        {
            if (!this.AwakeCondition && this is ConsciousnessAwake)
            {
                return ((ConsciousnessAwake)this).Sleep();
            }

            if (this.AwakeCondition && this is ConsciousnessAsleep)
            {
                return ((ConsciousnessAsleep)this).Awaken();
            }

        }

        public TimeSpan AwakeSpan
        {
            get
            {
                return DateTime.Now - this.SleepEndTime;
            }
        }

        public void AwakeNow(Consciousness consciousness)
        {
            consciousness.SleepEndTime = DateTime.Now;
        }

        public ConsciousnessAsleep Sleep(Consciousness consciousness)
        {
            this.AwakeNow(consciousness);

            if (consciousness.HasEverSlept)
            {
                var awakeSpan = consciousness.SleepStartTime - consciousness.SleepEndTime;
                consciousness.KnownAwakeSpan += awakeSpan;

                var console = this.GameInterop.Console;
                console.WriteLine(string.Format("MESSAGE: {0} in Awakening", awakeSpan.ToString("hh':'mm':'ss''")));
            }

            var @event = this.GameInterop.Event;
            @event.TriggerEvent(new Event((int)SessionEventType.SleepStarted, new ConsciousnessSleepStartedEventArgs(this)));

            return new ConsciousnessAsleep(consciousness);
        }

        public override void Update(GameTime time)
        {
            base.Update(time);
        }
    }
}