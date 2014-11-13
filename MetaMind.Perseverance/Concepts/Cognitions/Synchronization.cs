using MetaMind.Engine;
using MetaMind.Perseverance.Concepts.TaskEntries;
using Microsoft.Xna.Framework;
using System;
using System.Runtime.Serialization;

namespace MetaMind.Perseverance.Concepts.Cognitions
{
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

        void ResetForTomorrow();

        void TryStart(TaskEntry target);

        void Start(TaskEntry target);

        void Stop();

        void Update(GameTime gameTime);
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

        #endregion Read-only Properties

        #region Operations

        public void ResetForTomorrow()
        {
            statistics.Reset();
        }

        public void TryStart(TaskEntry target)
        {
            if (this.Enabled)
            {
                this.Stop();
            }

            this.Start(target);
        }

        public void Start(TaskEntry target)
        {
            data .Accept(target);
            timer.Start();
        }

        public void Stop()
        {
            TimeSpan timePassed;

            data      .Release(out timePassed);
            statistics.Add(timePassed);
            timer     .Stop();
        }

        public void Update(GameTime gameTime)
        {
            timer.Update(gameTime);
            data .Update(gameTime, timer.Enabled, Acceleration);
        }

        #endregion Operations
    }
}