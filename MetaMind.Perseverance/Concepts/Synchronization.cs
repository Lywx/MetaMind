namespace MetaMind.Perseverance.Concepts
{
    using System;
    using System.Linq;
    using System.Runtime.Serialization;

    using MetaMind.Engine;
    using MetaMind.Perseverance.Concepts.Tasks;

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

        ISynchronizable SynchronizedData { get; }

        TimeSpan SynchronizedTimeRecentWeek { get; }

        TimeSpan SynchronizedTimeToday { get; }

        TimeSpan SynchronizedTimeYesterday { get; }

        void TryAbort();

        void ResetTomorrow();

        void ResetToday();

        void Stop();

        void TryStart(ISynchronizable data);

        void Update();
    }

    [DataContract(Name = "Synchronization")]
    public class Synchronization : GameEngineAccess, ISynchronization
    {
        #region Components

        [DataMember]
        private readonly SynchronizationProcessor processor = new SynchronizationProcessor();

        [DataMember]
        private readonly SynchronizationDescription description = new SynchronizationDescription();

        [DataMember]
        private readonly SynchronizationStatistics statistics = new SynchronizationStatistics();

        [DataMember]
        private readonly SynchronizationTimer timer = new SynchronizationTimer();

        #endregion Components

        #region Read-only Properties

        public double Acceleration
        {
            get { return this.description.LevelAcceleration[this.Level]; }
        }

        public TimeSpan ElapsedTimeSinceTransition
        {
            get { return this.timer.ElapsedTimeSinceTransition; }
        }

        public bool Enabled
        {
            get { return this.timer.IsEnabled; }
        }

        public int Level
        {
            get
            {
                var level = 0;
                for (var i = 0; i < this.description.LevelSeconds.Length; ++i)
                {
                    if (this.timer.AccumulatedTime.TotalSeconds > this.description.LevelSeconds[i])
                    {
                        level = i;
                    }
                }
                return level;
            }
        }

        public double LevelSeconds
        {
            get { return this.description.LevelSeconds[this.Level]; }
        }

        public int NextLevel
        {
            get { return this.Level + 1; }
        }

        public double NextLevelSeconds
        {
            get { return this.description.LevelSeconds[this.NextLevel]; }
        }

        public double ProgressPercent
        {
            get
            {
                var left   = this.LevelSeconds;
                var right  = this.NextLevelSeconds;
                var middle = this.Seconds;

                return (middle - left) / (right - left);
            }
        }

        public double Seconds
        {
            get { return this.timer.AccumulatedTime.TotalSeconds; }
        }

        public string State
        {
            get { return this.description.LevelStates[this.Level]; }
        }

        public int SynchronizedHourMax
        {
            get { return this.statistics.HourMax; }
        }

        public int SynchronizedHourToday
        {
            get { return this.statistics.AccumulatedHourToday < this.SynchronizedHourMax ? this.statistics.AccumulatedHourToday : this.SynchronizedHourMax; }
        }

        public int SynchronizedHourYesterday
        {
            get { return this.statistics.AccumulatedHourYesterday < this.SynchronizedHourMax ? this.statistics.AccumulatedHourYesterday : this.SynchronizedHourMax; }
        }

        public ISynchronizable SynchronizedData
        {
            get { return this.processor.Data; }
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
            get { return this.statistics.AccumulatedTimeToday; }
        }

        public TimeSpan SynchronizedTimeYesterday
        {
            get { return this.statistics.AccumulatedTimeYesterday; }
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

        public void Start(ISynchronizable data)
        {
            this.processor.Accept(data);
            this.timer.Start();
        }

        public void Abort()
        {
            this.processor.Abort();
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
            TimeSpan validPassed = TimeSpan.FromHours(16);
            TimeSpan timePassed;

            this.processor.Release(out timePassed);

            if (timePassed < validPassed)
            {
                this.statistics.Add(timePassed);
            }
            else
            {
                // to avoid possible invalid time passed.
                this.statistics.Add(this.timer.ElapsedTimeSinceTransition);
            }

            this.timer.Stop();
        }

        public void TryStart(ISynchronizable data)
        {
            if (this.Enabled)
            {
                this.Stop();
            }

            this.Start(data);
        }

        public void Update()
        {
            this.timer.Update();
            this.processor .Update(this.timer.IsEnabled, this.Acceleration);
        }

        #endregion Operations
    }
}