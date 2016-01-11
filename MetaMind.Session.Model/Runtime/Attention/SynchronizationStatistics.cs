namespace MetaMind.Session.Model.Runtime.Attention
{
    using System;

    [DataContract]
    public class SynchronizationStatistics
    {
        [DataMember]
        public readonly int DayMax = 7;

        [DataMember]
        public readonly int HourMax = 16;

        public SynchronizationStatistics()
        {
            this.AccumulatedTimeWeekday = new TimeSpan[this.DayMax];
            this.AccumulatedTimeToday   = TimeSpan.Zero;
        }

        public int AccumulatedHourToday => (int)this.AccumulatedTimeToday.TotalHours;

        [DataMember]
        public int AccumulatedHourYesterday { get; set; }

        [DataMember]
        public TimeSpan AccumulatedTimeToday { get; private set; }

        [DataMember]
        public TimeSpan[] AccumulatedTimeWeekday { get; private set; } 
        [DataMember]
        public TimeSpan AccumulatedTimeYesterday { get; private set; }

        public void Add(TimeSpan accumulatedTime)
        {
            this.AccumulatedTimeToday = this.AccumulatedTimeToday + accumulatedTime;
        }

        public void ResetDaily()
        {
            // save today to yesterday
            this.AccumulatedHourYesterday = this.AccumulatedHourToday;
            this.AccumulatedTimeYesterday = this.AccumulatedTimeToday;

            // shift forward record
            // 0-index is latest, 6-index is oldest
            Array.Copy(this.AccumulatedTimeWeekday, 0, this.AccumulatedTimeWeekday, 1, this.DayMax - 1);
            this.AccumulatedTimeWeekday[0] = this.AccumulatedTimeYesterday;
            
            this.ResetToday();
        }

        /// <summary>
        /// Clear today's record.
        /// </summary>
        public void ResetToday()
        {
            this.AccumulatedTimeToday = TimeSpan.Zero;
        }
    }
}