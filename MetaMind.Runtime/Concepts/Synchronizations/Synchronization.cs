namespace MetaMind.Runtime.Concepts.Synchronizations
{
    using System;
    using System.Linq;
    using System.Runtime.Serialization;

    [DataContract]
    public class Synchronization : ISynchronization
    {
        #region Components

        [DataMember]
        private readonly SynchronizationDescription description = new SynchronizationDescription();

        [DataMember]
        private readonly SynchronizationProcessor processor = new SynchronizationProcessor();

        [DataMember]
        private readonly SynchronizationStatistics statistics = new SynchronizationStatistics();

        [DataMember]
        private readonly SynchronizationTimer timer = new SynchronizationTimer();

        #endregion Components

        #region Constructors

        public Synchronization()
        {
        }

        #endregion

        #region State Data

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

        public float Progress
        {
            get
            {
                var left   = this.LevelSeconds;
                var right  = this.NextLevelSeconds;
                var middle = this.timer.AccumulatedTime.TotalSeconds;

                return (float)((middle - left) / (right - left));
            }
        }

        public string State
        {
            get
            {
                return this.description.LevelStates[this.Level];
            }
        }

        private double LevelSeconds
        {
            get { return this.description.LevelSeconds[this.Level]; }
        }

        private int NextLevel
        {
            get { return this.Level + 1; }
        }

        private double NextLevelSeconds
        {
            get { return this.description.LevelSeconds[this.NextLevel]; }
        }

        #endregion

        #region Statistic Data

        public int SynchronizedHourMax
        {
            get
            {
                return this.statistics.HourMax;
            }
        }

        public int SynchronizedHourToday
        {
            get
            {
                return this.statistics.AccumulatedHourToday < this.SynchronizedHourMax
                           ? this.statistics.AccumulatedHourToday
                           : this.SynchronizedHourMax;
            }
        }

        public int SynchronizedHourYesterday
        {
            get
            {
                return this.statistics.AccumulatedHourYesterday < this.SynchronizedHourMax
                           ? this.statistics.AccumulatedHourYesterday
                           : this.SynchronizedHourMax;
            }
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
            get
            {
                return this.statistics.AccumulatedTimeToday;
            }
        }

        public TimeSpan SynchronizedTimeTodayBestCase
        {
            get
            {
                return this.SynchronizedTimeToday + (this.Enabled ? this.ElapsedTimeSinceTransition : TimeSpan.Zero);
            }
        }

        public TimeSpan SynchronizedTimeYesterday
        {
            get
            {
                return this.statistics.AccumulatedTimeYesterday;
            }
        }

        #endregion Read-only Properties

        #region Operations

        public void Abort()
        {
            this.processor.Abort();
            this.timer    .Stop();
        }

        public void ResetToday()
        {
            this.statistics.ResetToday();
        }

        public void ResetTomorrow()
        {
            this.statistics.ResetDaily();
        }

        public void Start(ISynchronizationData data)
        {
            this.processor.Accept(data);
            this.timer    .Start();
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
                // To avoid possible invalid time passed.
                this.statistics.Add(this.timer.ElapsedTimeSinceTransition);
            }

            this.timer.Stop();
        }

        public void TryAbort()
        {
            if (this.Enabled)
            {
                this.Abort();
            }
        }

        public void TryStart(ISynchronizationData data)
        {
            if (this.Enabled)
            {
                this.Stop();
            }

            this.Start(data);
        }

        public void Update()
        {
            this.timer    .Update();
            this.processor.Update(this.timer.IsEnabled, this.Acceleration);
        }

        #endregion Operations
    }
}