// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Event.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin  
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Component.Interop.Event
{
    public class Event : IEvent
    {
        /// <summary>
        /// Base class for Game Events
        /// </summary>
        /// <param name="eventType">The type of event being sent</param>
        /// <param name="eventData">Any type of data that needs to go with the event.  Can be an object, a value, null, etc</param>
        /// <param name="eventLife">The length in Seconds the event should stay alive if not picked up</param>
        public Event(int eventType, object eventData, int eventLife = 1, int handleAttempts = 0)
        {
            this.EventType      = eventType;
            this.EventData      = eventData;
            this.EventLife      = eventLife;
            this.HandleAttempts = handleAttempts;
        }

        public int EventType { get; set; }

        public object EventData { get; set; }

        public long CreationTime { get; set; }

        public int EventLife { get; set; }

        public bool Handled { get; set; }

        public int HandleAttempts { get; set; }
    }
}