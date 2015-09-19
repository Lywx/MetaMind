namespace MetaMind.Engine.Component.Event
{
    using System.Collections.Generic;

    public interface IListener
    {
        List<int> RegisteredEvents { get; }

        bool HandleEvent(IEvent e);
    }
}