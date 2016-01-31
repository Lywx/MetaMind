namespace MetaMind.Engine.Core.Backend.Interop.Event
{
    using System.Collections.Generic;

    public interface IMMEventListener
    {
        List<int> RegisteredEvents { get; }

        bool HandleEvent(IMMEvent e);
    }
}