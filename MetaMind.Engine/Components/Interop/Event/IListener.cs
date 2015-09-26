namespace MetaMind.Engine.Components.Interop.Event
{
    using System.Collections.Generic;

    public interface IListener
    {
        List<int> RegisteredEvents { get; }

        bool HandleEvent(IEvent e);
    }
}