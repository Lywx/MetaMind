namespace MetaMind.Session.Model.Runtime.Attention
{
    using System;

    public interface ISynchronizationMetadata
    {
        string State { get; }

        bool Enabled { get; }

        int Level { get; }

        float Progress { get; }

        float Acceleration { get; }

        TimeSpan ElapsedTimeSinceTransition { get; }
    }
}