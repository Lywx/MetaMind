using System;
using System.Runtime.Serialization;

namespace MetaMind.Perseverance.Concepts.Cognitions
{
    [DataContract]
    public class SynchronizationDailyStatistics
    {
        //---------------------------------------------------------------------
        [DataMember]
        public readonly int HourMax = 14;
        [DataMember]
        private         TimeSpan accumulatedTimeToday = TimeSpan.Zero;

        //---------------------------------------------------------------------
        public int AccumulatedHourToday     { get { return ( int ) accumulatedTimeToday.TotalHours; } }
        [DataMember]
        public int AccumulatedHourYesterday { get; set; }


        public void Add( TimeSpan accumulatedTime )
        {
            accumulatedTimeToday += accumulatedTime;
        }

        public void Reset()
        {
            AccumulatedHourYesterday = AccumulatedHourToday;

            accumulatedTimeToday = TimeSpan.Zero;
        }
    }
}