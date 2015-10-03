namespace MetaMind.Engine.Components.Interop.Event
{
    using System.Collections.Generic;

    public interface IMMEventListener
    {
        List<int> RegisteredEvents { get; }

        bool HandleEvent(IMMEvent e);
    }
}