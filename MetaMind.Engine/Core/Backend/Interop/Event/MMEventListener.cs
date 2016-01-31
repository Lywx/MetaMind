// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Listener.cs">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Core.Backend.Interop.Event
{
    using System;
    using System.Collections.Generic;
    using Entity.Common;

    public class MMEventListener : MMEntity, IMMEventListener
    {
        private readonly List<int> registeredEvents;

        private readonly Func<IMMEvent, bool> handleEvents;

        public MMEventListener(List<int> registeredEvents, Func<IMMEvent, bool> handleEvents)
        {
            this.handleEvents     = handleEvents;
            this.registeredEvents = registeredEvents;
        }

        protected MMEventListener()
        {
            this.registeredEvents = new List<int>();
        }

        public List<int> RegisteredEvents => this.registeredEvents;

        public virtual bool HandleEvent(IMMEvent e)
        {
            if (this.handleEvents != null)
            {
                return this.handleEvents(e);
            }

            return false;
        }
    }
}