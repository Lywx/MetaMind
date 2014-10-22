using System.Collections.Generic;

namespace MetaMind.Engine.Components.Events
{
    public class ListenerBase : EngineObject
    {
        protected ListenerBase()
        {
            registeredEvents = new List<int>();
        }

        private List<int> registeredEvents;

        public List<int> RegisteredEvents
        {
            get { return registeredEvents; }
        }

        public virtual bool HandleEvent(EventBase @event)
        {
            return false;
        }
    }
}