namespace MetaMind.Acutance.Concepts
{
    using System;
    using System.Runtime.Serialization;

    using MetaMind.Engine;
    using MetaMind.Engine.Concepts;

    [DataContract]
    [KnownType(typeof(CommandTimerWithDate))]
    [KnownType(typeof(CommandTimerWithTimeout))]
    public abstract class CommandTimer : GameEngineAccess
    {
        [DataMember]
        public readonly TimeSpan Transition = TimeSpan.FromSeconds(30);

        [DataMember]
        public SynchronizationSpan SynchronizationSpan { get; protected set; }

        public abstract bool IsAutoReseting { get; }

        public abstract bool IsTransiting { get; }

        public override void Dispose()
        {
            this.SynchronizationSpan = SynchronizationSpan.Zero;

            base.Dispose();
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