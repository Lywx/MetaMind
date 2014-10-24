using System;
using System.Runtime.Serialization;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Concepts.Cognitions
{
    [DataContract]
    public class SynchronizationTimer
    {
        //---------------------------------------------------------------------
        [DataMember]
        private DateTime currentMoment = DateTime.Now;
        [DataMember]
        private DateTime previousMoment;
        [DataMember]
        private DateTime transitionMoment;
        [DataMember]
        private bool     enabled;

        //---------------------------------------------------------------------
        public SynchronizationTimer()
        {
            AccumulatedTime = TimeSpan.Zero;
        }

        //---------------------------------------------------------------------

        [DataMember]
        public TimeSpan AccumulatedTime { get; set; }

        public TimeSpan ElapsedTimeSinceTransition
        {
            get { return transitionMoment.Ticks != 0 ? currentMoment - transitionMoment : TimeSpan.Zero; }
        }

        private TimeSpan ElapsedTime
        {
            get { return currentMoment - previousMoment; }
        }

        public bool Enabled
        {
            get { return enabled; }
        }

        public void Start()
        {
            Transition();
        }

        public void Stop()
        {
            Transition();
        }

        public void Update( GameTime gameTime )
        {
            // record time regardless whether update get called
            previousMoment = currentMoment;
            currentMoment  = DateTime.Now;

            if ( Enabled )
                AccumulatedTime += ElapsedTime;
            else
            {
                AccumulatedTime -= ElapsedTime;
                if ( AccumulatedTime.TotalSeconds < 0 )
                    AccumulatedTime = TimeSpan.Zero;
            }
        }

        private void Transition()
        {
            enabled = !Enabled;
            transitionMoment = DateTime.Now;
        }
    }
}