namespace MetaMind.Session.Model.Runtime.Attention
{
    using System;
    using System.Linq;
    using Process;

    [DataContract]
    public class SynchronizationData : ISynchronizationData
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

        public SynchronizationData()
        {
        }

        #endregion

        #region State Data

        public double Acceleration => this.description.LevelAcceleration[this.Level];

        public TimeSpan ElapsedTimeSinceTransition => this.timer.ElapsedTimeSinceTransition;

        public bool Enabled => this.timer.IsEnabled;

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

        public string State => this.description.LevelStates[this.Level];

        private double LevelSeconds => this.description.LevelSeconds[this.Level];

        private int NextLevel => this.Level + 1;

        private double NextLevelSeconds => this.description.LevelSeconds[this.NextLevel];

        #endregion

        #region Statistic Data

        public int SynchronizedHourMax => this.statistics.HourMax;

        public int SynchronizedHourToday => this.statistics.AccumulatedHourToday < this.SynchronizedHourMax
                                                ? this.statistics.AccumulatedHourToday
                                                : this.SynchronizedHourMax;

        public int SynchronizedHourYesterday => this.statistics.AccumulatedHourYesterday < this.SynchronizedHourMax
                                                    ? this.statistics.AccumulatedHourYesterday
                                                    : this.SynchronizedHourMax;

        public TimeSpan SynchronizedTimeRecentWeek
        {
            get
            {
                return new TimeSpan(this.statistics.AccumulatedTimeWeekday.Sum(r => r.Duration().Ticks));
            }
        }

        public TimeSpan SynchronizedTimeToday => this.statistics.AccumulatedTimeToday;

        public TimeSpan SynchronizedTimeTodayBestCase => this.SynchronizedTimeToday + (this.Enabled ? this.ElapsedTimeSinceTransition : TimeSpan.Zero);

        public TimeSpan SynchronizedTimeYesterday => this.statistics.AccumulatedTimeYesterday;

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

        public void Begin(IJobSynchronizationData data)
        {
            this.processor.Attach(data);
            this.timer    .Begin();
        }

        public void Stop()
        {
            TimeSpan validPassed = TimeSpan.FromHours(16);
            TimeSpan timePassed;

            this.processor.Detach(out timePassed);

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

        public void TryBegin(IJobSynchronizationData data)
        {
            if (this.Enabled)
            {
                this.Stop();
            }

            this.Begin(data);
        }

        public void Update()
        {
            this.timer    .Update();
            this.processor.Update(this.timer.IsEnabled, this.Acceleration);
        }

        #endregion Operations
    }
}