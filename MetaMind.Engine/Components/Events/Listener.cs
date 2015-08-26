// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Listener.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Components.Events
{
    using System;
    using System.Collections.Generic;

    public class Listener : GameEntity, IListener
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