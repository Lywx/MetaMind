namespace MetaMind.Unity.Concepts.Synchronizations
{
    using System;
    using Engine;

    public interface ISynchronization : IInnerUpdatable, ISynchronizationOperations
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
    }
}