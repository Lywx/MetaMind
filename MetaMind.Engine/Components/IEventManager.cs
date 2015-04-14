namespace MetaMind.Engine.Components
{
    using MetaMind.Engine.Components.Events;

    public interface IEventManager
    {
        void AddListener(IListener listener);

        void RemoveListener(IListener listener);

        void QueueEvent(IEvent e);

        void QueueUniqueEvent(IEvent e);

        void RemoveQueuedEvent(IEvent e, bool allOccurances);

        void TriggerEvent(IEvent e);
    }
}