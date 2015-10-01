// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Listener.cs">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Components.Interop.Event
{
    using System;
    using System.Collections.Generic;
    using Entities;

    public class Listener : MMEntity, IListener
    {
        private readonly List<int> registeredEvents;

        private readonly Func<IEvent, bool> handleEvents;

        public Listener(List<int> registeredEvents, Func<IEvent, bool> handleEvents)
        {
            this.handleEvents     = handleEvents;
            this.registeredEvents = registeredEvents;
        }

        protected Listener()
        {
            this.registeredEvents = new List<int>();
        }

        public List<int> RegisteredEvents => this.registeredEvents;

        public virtual bool HandleEvent(IEvent e)
        {
            if (this.handleEvents != null)
            {
                return this.handleEvents(e);
            }

            return false;
        }
    }
}