namespace MetaMind.Perseverance.Concepts.Cognitions
{
    using System;
    using System.Linq;
    using System.Runtime.Serialization;

    using MetaMind.Engine;
    using MetaMind.Perseverance.Concepts.TaskEntries;

    public interface ISynchronization
    {
        double Acceleration { get; }

        TimeSpan ElapsedTimeSinceTransition { get; }

        bool Enabled { get; }

        int Level { get; }

        double ProgressPercent { get; }

        string State { get; }

        int SynchronizedHourMax { get; }

        int SynchronizedHourToday { get; }

        int SynchronizedHourYesterday { get; }

        TaskEntry SynchronizedTask { get; }

        TimeSpan SynchronizedTimeRecentWeek { get; }

        TimeSpan SynchronizedTimeToday { get; }

        TimeSpan SynchronizedTimeYesterday { get; }

        void TryAbort();

        void ResetTomorrow();

        void ResetToday();

        void Stop();

        void TryStart(TaskEntry target);

        void Update();
    }

    [DataContract]
    public class Synchronization : EngineObject, ISynchronization
    {
        #region Components

        [DataMember]
        private readonly SynchronizationDataProcessor   data        = new SynchronizationDataProcessor();

        [DataMember]
        private readonly SynchronizationDescription     description = new SynchronizationDescription();

        [DataMember]
        private readonly SynchronizationDailyStatistics statistics  = new SynchronizationDailyStatistics();

        [DataMember]
        private readonly SynchronizationTimer           timer       = new SynchronizationTimer();

        #endregion Components

        #region Read-only Properties

        public double Acceleration
        {
            get { return description.LevelAcceleration[Level]; }
        }

        public TimeSpan ElapsedTimeSinceTransition
        {
            get { return timer.ElapsedTimeSinceTransition; }
        }

        public bool Enabled
        {
            get { return timer.Enabled; }
        }

        public int Level
        {
            get
            {
                var level = 0;
                for (var i = 0; i < description.LevelSeconds.Length; ++i)
                {
                    if (timer.AccumulatedTime.TotalSeconds > description.LevelSeconds[i])
                    {
                        level = i;
                    }
                }
                return level;
            }
        }

        public double LevelSeconds
        {
            get { return description.LevelSeconds[Level]; }
        }

        public int NextLevel
        {
            get { return Level + 1; }
        }

        public double NextLevelSeconds
        {
            get { return description.LevelSeconds[NextLevel]; }
        }

        public double ProgressPercent
        {
            get
            {
                var left   = LevelSeconds;
                var right  = NextLevelSeconds;
                var middle = Seconds;

                return (middle - left) / (right - left);
            }
        }

        public double Seconds
        {
            get { return timer.AccumulatedTime.TotalSeconds; }
        }

        public string State
        {
            get { return description.LevelStates[Level]; }
        }

        public int SynchronizedHourMax
        {
            get { return statistics.HourMax; }
        }

        public int SynchronizedHourToday
        {
            get { return statistics.AccumulatedHourToday; }
        }

        public int SynchronizedHourYesterday
        {
            get { return statistics.AccumulatedHourYesterday; }
        }

        public TaskEntry SynchronizedTask
        {
            get { return data.Target; }
        }

        public TimeSpan SynchronizedTimeRecentWeek
        {
            get
            {
                return new TimeSpan(this.statistics.AccumulatedTimeWeekday.Sum(r => r.Duration().Ticks));
            }
        }

        public TimeSpan SynchronizedTimeToday
        {
            get { return statistics.AccumulatedTimeToday; }
        }

        public TimeSpan SynchronizedTimeYesterday
        {
            get { return statistics.AccumulatedTimeYesterday; }
        }

        #endregion Read-only Properties

        #region Operations

        public void ResetTomorrow()
        {
            this.statistics.ResetDaily();
        }

        public void ResetToday()
        {
            this.statistics.ResetToday();
        }

        public void Start(TaskEntry target)
        {
            this.data .Accept(target);
            this.timer.Start();
        }

        public void Abort()
        {
            this.data .Abort();
            this.timer.Stop();
        }

        public void TryAbort()
        {
            if (this.Enabled)
            {
                this.Abort();
            }
        }

        public void Stop()
        {
            TimeSpan timePassed;

            this.data      .Release(out timePassed);
            this.statistics.Add(timePassed);
            this.timer     .Stop();
        }

        public void TryStart(TaskEntry target)
        {
            if (this.Enabled)
            {
                this.Stop();
            }

            this.Start(target);
        }

        public void Update()
        {
            this.timer.Update();
            this.data .Update(timer.Enabled, Acceleration);
        }

        #endregion Operations
    }
}