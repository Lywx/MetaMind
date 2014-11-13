using System;
using System.Runtime.Serialization;

namespace MetaMind.Perseverance.Concepts.Cognitions
{
    [DataContract]
    public class SynchronizationDailyStatistics
    {
        [DataMember]
        public readonly int HourMax = 14;

        [DataMember]
        private         TimeSpan accumulatedTimeToday = TimeSpan.Zero;

        public int AccumulatedHourToday { get { return (int)this.accumulatedTimeToday.TotalHours; } }

        [DataMember]
        public int AccumulatedHourYesterday { get; set; }

        public void Add(TimeSpan accumulatedTime)
        {
            this.accumulatedTimeToday += accumulatedTime;
        }

        public void Reset()
        {
            this.AccumulatedHourYesterday = AccumulatedHourToday;

            this.accumulatedTimeToday = TimeSpan.Zero;
        }
    }
}