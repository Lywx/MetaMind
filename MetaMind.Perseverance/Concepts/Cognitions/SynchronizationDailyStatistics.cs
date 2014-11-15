namespace MetaMind.Perseverance.Concepts.Cognitions
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class SynchronizationDailyStatistics
    {
        [DataMember]
        public readonly int HourMax = 16;

        [DataMember]
        public readonly int DayMax = 7;

        public SynchronizationDailyStatistics()
        {
            this.AccumulatedTimeWeekday = new TimeSpan[this.DayMax];
            this.AccumulatedTimeToday = TimeSpan.Zero;
        }

        public int AccumulatedHourToday { get { return (int)this.AccumulatedTimeToday.TotalHours; } }

        [DataMember]
        public int AccumulatedHourYesterday { get; set; }

        [DataMember]
        public TimeSpan[] AccumulatedTimeWeekday { get; private set; } 
        
        [DataMember]
        public TimeSpan AccumulatedTimeToday { get; private set; }

        public void Add(TimeSpan accumulatedTime)
        {
            this.AccumulatedTimeToday = this.AccumulatedTimeToday + accumulatedTime;
        }

        public void Reset()
        {
            Array.Copy(this.AccumulatedTimeWeekday, 1, this.AccumulatedTimeWeekday, 0, this.DayMax);
            Array.Copy(this.AccumulatedTimeWeekday, this.AccumulatedTimeWeekday, 1);
            
            this.AccumulatedHourYesterday = this.AccumulatedHourToday;

            this.AccumulatedTimeToday = TimeSpan.Zero;
        }
    }
}