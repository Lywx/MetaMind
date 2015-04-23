namespace MetaMind.Perseverance.Concepts
{
    using System;

    public interface ISynchronization
    {
        #region State Data

        string State { get; }

        bool Enabled { get; }

        int Level { get; }

        double Progress { get; }

        double Acceleration { get; }

        TimeSpan ElapsedTimeSinceTransition { get; }

        ISynchronizable SynchronizedData { get; }

        #endregion

        #region Statistics
        
        int SynchronizedHourMax { get; }

        int SynchronizedHourToday { get; }

        int SynchronizedHourYesterday { get; }

        TimeSpan SynchronizedTimeRecentWeek { get; }

        TimeSpan SynchronizedTimeYesterday { get; }

        TimeSpan SynchronizedTimeToday { get; }

        TimeSpan PotentialSynchronizedTimeToday { get; }

        #endregion

        #region Operations

        void TryAbort();

        void ResetTomorrow();

        void ResetToday();

        void Stop();

        void TryStart(ISynchronizable data);

        #endregion

        #region Update

        void Update();

        #endregion
    }
}