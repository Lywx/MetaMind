namespace MetaMind.Session.Model.Runtime.Attention
{
    using System;

    public interface ISynchronizationBase : IMMFreeUpdatable
    {
        
    }

    public interface ISynchronizationData : ISynchronizationBase, ISynchronizationOperations
    {
        int SynchronizedHourMax { get; }

        int SynchronizedHourToday { get; }

        int SynchronizedHourYesterday { get; }

        TimeSpan SynchronizedTimeRecentWeek { get; }

        TimeSpan SynchronizedTimeYesterday { get; }

        TimeSpan SynchronizedTimeToday { get; }

        TimeSpan SynchronizedTimeTodayBestCase { get; }
    }
}