namespace MetaMind.Unity.Concepts.Synchronizations
{
    using Engine;
    using Engine.Components.Interop.Event;
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
            var @event = this.Interop.Event;
            @event.QueueEvent(new Event((int)SessionEvent.SyncStarted, new SynchronizationStartedEventArgs(this.SynchronizationData)));
        }

        public void StopSynchronization()
        {
            var @event = this.Interop.Event;
            @event.QueueEvent(new Event((int)SessionEvent.SyncStopped, new SynchronizationStoppedEventArgs(this.SynchronizationData)));
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