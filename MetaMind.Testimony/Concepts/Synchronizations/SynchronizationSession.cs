namespace MetaMind.Testimony.Concepts.Synchronizations
{
    using Engine;
    using Engine.Components.Events;
    using Events;
    using Sessions;

    public class SynchronizationSession : GameEntity, ISynchronizationController
    {
        public SynchronizationSession()
        {
            this.SynchronizationData = new SynchronizationData();
        }

        protected ISynchronizationData SynchronizationData { get; private set; }

        public void StartSynchronization()
        {
            var @event = this.GameInterop.Event;
            @event.QueueEvent(new Event((int)SessionEventType.SyncStarted, new SynchronizationStartedEventArgs(this.SynchronizationData)));
        }

        public void StopSynchronization()
        {
            var @event = this.GameInterop.Event;
            @event.QueueEvent(new Event((int)SessionEventType.SyncStopped, new SynchronizationStoppedEventArgs(this.SynchronizationData)));
        }

        public void ToggleSynchronization()
        {
            if (!this.SynchronizationData.IsSynchronizing)
            {
                this.StartSynchronization();
            }
            else
            {
                this.StopSynchronization();
            }
        }
    }
}