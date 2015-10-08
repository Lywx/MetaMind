namespace MetaMind.Session.Concepts.Synchronizations
{
    using Engine.Components.Interop.Event;
    using Engine.Entities;
    using Session.Sessions;

    public class SynchronizationSession : MMEntity, ISynchronizationController
    {
        public SynchronizationSession()
        {
            this.SynchronizationData = new SynchronizationData();
        }

        protected ISynchronizationData SynchronizationData { get; private set; }

        public void StartSynchronization()
        {
            var @event = this.Interop.Event;
            @event.QueueEvent(new MMEvent((int)SessionEvent.SyncStarted, new SynchronizationStartedEventArgs(this.SynchronizationData)));
        }

        public void StopSynchronization()
        {
            var @event = this.Interop.Event;
            @event.QueueEvent(new MMEvent((int)SessionEvent.SyncStopped, new SynchronizationStoppedEventArgs(this.SynchronizationData)));
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