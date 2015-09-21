namespace MetaMind.Engine.Component.Interop.Event
{
    using System;
    using Microsoft.Xna.Framework;

    public interface IEventManager : IGameComponent, IDisposable
    {
        void AddListener(IListener listener);

        void RemoveListener(IListener listener);

        void QueueEvent(IEvent e);

        void QueueUniqueEvent(IEvent e);

        void RemoveQueuedEvent(IEvent e, bool allOccurances);

        void TriggerEvent(IEvent e);
    }
}