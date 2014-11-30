// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventManager.cs" company="UESTC">
//   Copyright (c) 2014 Lin Wuxiang
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Components
{
    using MetaMind.Engine.Components.Events;
    using Microsoft.Xna.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class EventManager : GameComponent
    {
        // ---------------------------------------------------------------------

        // ---------------------------------------------------------------------
        private List<EventBase> activeEvents;

        private List<int> knownEvents;

        private List<ListenerBase> listeners;

        private List<EventBase> queuedEvents;

        public List<int> KnownEvents
        {
            get
            {
                return this.knownEvents;
            }
        }

        public List<ListenerBase> Listeners
        {
            get
            {
                return this.listeners;
            }
        }

        #region Singleton

        private static EventManager singleton;

        public static EventManager GetInstance(Game game)
        {
            if (singleton == null)
            {
                singleton = new EventManager(game);
            }

            if (game != null)
            {
                game.Components.Add(singleton);
            }

            return singleton;
        }

        #endregion Singleton

        #region Constructors

        /// <summary>
        /// Handles all events
        private EventManager(Game game)
            : base(game)
        {
        }

        #endregion Constructors

        #region Initialization

        public override void Initialize()
        {
            this.knownEvents  = new List<int>();
            this.queuedEvents = new List<EventBase>();
            this.activeEvents = new List<EventBase>();
            this.listeners    = new List<ListenerBase>();
        }

        #endregion Initialization

        #region Listener Operations

        public void AddListener(ListenerBase listener)
        {
            if (!this.listeners.Contains(listener))
            {
                this.listeners.Add(listener);
                foreach (var eventType in listener.RegisteredEvents)
                {
                    this.AddKnownEvent(eventType);
                }
            }
        }

        public void RemoveListener(ListenerBase listener)
        {
            if (this.listeners.Contains(listener))
            {
                this.listeners.Remove(listener);
            }
        }

        #endregion Listener Operations

        #region Event Operations

        public void QueueEvent(EventBase @event)
        {
            if (this.ValidateEvent(@event.EventType))
            {
                @event.CreationTime = DateTime.Now.Ticks;
                this.queuedEvents.Add(@event);
            }
        }

        public void QueueUniqueEvent(EventBase @event)
        {
            if (this.ValidateEvent(@event.EventType))
            {
                for (var x = this.queuedEvents.Count - 1; x >= 0; x--)
                {
                    if (this.queuedEvents[x].EventType == @event.EventType)
                    {
                        this.queuedEvents.RemoveAt(x);
                    }
                }

                this.queuedEvents.Add(@event);
            }
        }

        public void RemoveQueuedEvent(EventBase @event, bool allOccurances)
        {
            if (this.queuedEvents.Contains(@event))
            {
                if (allOccurances)
                {
                    for (var x = this.queuedEvents.Count; x >= 0; x--)
                    {
                        this.queuedEvents.RemoveAt(x);
                    }
                }
                else
                {
                    this.queuedEvents.Remove(@event);
                }
            }
        }

        public void TriggerEvent(EventBase @event)
        {
            if (this.ValidateEvent(@event.EventType))
            {
                foreach (var listener in
                    this.listeners.Where(listener => listener.RegisteredEvents.Contains(@event.EventType)).ToList())
                {
                    listener.HandleEvent(@event);
                }
            }
        }

        public bool ValidateEvent(int eventType)
        {
            return this.knownEvents.Contains(eventType);
        }

        private void AddKnownEvent(int eventType)
        {
            if (!this.knownEvents.Contains(eventType))
            {
                this.knownEvents.Add(eventType);
            }
        }

        #endregion Event Operations

        #region Update

        public override void Update(GameTime gameTime)
        {
            if (this.queuedEvents.Count == 0)
            {
                return;
            }

            var startTime = DateTime.Now.Ticks;

            // Max of 5 milliseconds of processing time. 1ms = 10,000ticks
            var stopTime = startTime + 50000;

            // Copy the queued events to the active events list
            this.activeEvents = new List<EventBase>(this.queuedEvents);
            this.queuedEvents.Clear();

            // Process at least one event..or so Mr. McShaffry says
            var counter = this.activeEvents.Count - 1;
            do
            {
                if (counter < 0)
                {
                    break;
                }

                var currentTime = DateTime.Now.Ticks;
                var @event = this.activeEvents[counter];

                @event.HandleAttempts++;

                for (var x = this.listeners.Count - 1; x >= 0; x--)
                {
                    if (this.listeners[x].RegisteredEvents.Contains(@event.EventType)
                        && this.listeners[x].HandleEvent(@event))
                    {
                        @event.Handled = true;
                    }
                }

                // If an event has been around for longer than 3 seconds, remove it
                // should change to 2 attempts?
                if (currentTime >= @event.CreationTime + this.SecondsToTicks(@event.LifeTime))
                {
                    @event.Handled = true;
                }

                if (@event.Handled)
                {
                    this.activeEvents.Remove(@event);
                    @event = null;
                }

                counter--;

                if (DateTime.Now.Ticks >= stopTime)
                {
                    break;
                }
            }
            while (this.activeEvents.Count > 0);

            // Add back any unhandled events
            if (this.activeEvents.Count > 0)
            {
                this.queuedEvents.AddRange(this.activeEvents);
            }
        }

        #endregion Update

        #region Time Helper

        private long SecondsToTicks(int second)
        {
            return second * TimeSpan.TicksPerSecond;
        }

        #endregion Time Helper

        // ---------------------------------------------------------------------

        // ---------------------------------------------------------------------
    }
}