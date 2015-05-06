namespace MetaMind.Testimony.Concepts.Synchronizations
{
    using System;

    public interface ISynchronization
    {
        #region State Data

        string State { get; }

        bool Enabled { get; }

        int Level { get; }

        float Progress { get; }

        double Acceleration { get; }

        TimeSpan ElapsedTimeSinceTransition { get; }

        #endregion

        #region Statistics
        
        int SynchronizedHourMax { get; }

        int SynchronizedHourToday { get; }

        int SynchronizedHourYesterday { get; }

        TimeSpan SynchronizedTimeRecentWeek { get; }

        TimeSpan SynchronizedTimeYesterday { get; }

        TimeSpan SynchronizedTimeToday { get; }

        TimeSpan SynchronizedTimeTodayBestCase { get; }

        #endregion

        #region Operations

        void TryAbort();

        void ResetTomorrow();

        void ResetToday();

        void Stop();

        void TryStart(ISynchronizationData data);

        #endregion

        #region Update

        void Update();

        #endregion
    }
}