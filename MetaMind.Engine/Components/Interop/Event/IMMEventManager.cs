namespace MetaMind.Engine.Components.Interop.Event
{
    using System;
    using Microsoft.Xna.Framework;

    public interface IMMEventManager : IGameComponent, IDisposable
    {
        void AddListener(IMMEventListener listener);

        void RemoveListener(IMMEventListener listener);

        void QueueEvent(IMMEvent e);

        void QueueUniqueEvent(IMMEvent e);

        void RemoveQueuedEvent(IMMEvent e, bool allOccurances);

        void TriggerEvent(IMMEvent e);
    }
}