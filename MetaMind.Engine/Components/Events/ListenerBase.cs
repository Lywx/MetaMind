// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ListenerBase.cs" company="UESTC">
//   Copyright (c) 2014 Lin Wuxiang
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Components.Events
{
    using System.Collections.Generic;

    public class ListenerBase : EngineObject
    {
        private readonly List<int> registeredEvents;

        protected ListenerBase()
        {
            registeredEvents = new List<int>();
        }

        public List<int> RegisteredEvents
        {
            get
            {
                return registeredEvents;
            }
        }

        public virtual bool HandleEvent(EventBase @event)
        {
            return false;
        }
    }
}