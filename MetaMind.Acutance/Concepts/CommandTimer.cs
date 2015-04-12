namespace MetaMind.Acutance.Concepts
{
    using System;
    using System.Runtime.Serialization;

    using MetaMind.Engine.Concepts;

    [DataContract]
    [KnownType(typeof(CommandTimerWithDate))]
    [KnownType(typeof(CommandTimerWithTimeout))]
    public abstract class CommandTimer : IDisposable
    {
        [DataMember]
        public readonly TimeSpan Transition = TimeSpan.FromSeconds(30);

        [DataMember]
        public SynchronizationSpan SynchronizationSpan { get; protected set; }

        public abstract bool IsAutoReseting { get; }

        public abstract bool IsTransiting { get; }

        public virtual void Dispose()
        {
            this.SynchronizationSpan = SynchronizationSpan.Zero;
        }

        [OnDeserialized]
        public void OnDeserialized(StreamingContext context)
        {
            this.Reset();
        }

        public abstract void Reset();

        public abstract void Update();
    }
}