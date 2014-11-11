using MetaMind.Engine.Components.Events;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MetaMind.Engine.Components
{
    public class EventManager : Microsoft.Xna.Framework.GameComponent
    {
        //---------------------------------------------------------------------
        private static EventManager singleton;

        //---------------------------------------------------------------------

        private List<int>          knownEvents;
        private List<EventBase>    activeEvents;
        private List<EventBase>    queuedEvents;
        private List<ListenerBase> listeners;

        public List<int> KnownEvents
        {
            get { return knownEvents; }
        }

        public List<ListenerBase> Listeners
        {
            get { return listeners; }
        }

        public bool ValidateEvent( int eventType )
        {
            return knownEvents.Contains( eventType );
        }

        //---------------------------------------------------------------------

        #region Constructors

        /// <summary>
        /// Handles all events
        private EventManager( Game game )
            : base( game )
        {
        }

        #endregion Constructors

        //---------------------------------------------------------------------

        #region Initialization

        public static EventManager GetInstance( Game game )
        {
            if ( singleton == null )
                singleton = new EventManager( game );
            if ( game != null )
                game.Components.Add( singleton );
            return singleton;
        }

        public override void Initialize()
        {
            knownEvents = new List<int>();
            queuedEvents = new List<EventBase>();
            activeEvents = new List<EventBase>();
            listeners = new List<ListenerBase>();
        }

        #endregion Initialization

        #region Operations

        public void AddListener( ListenerBase listener )
        {
            if ( !listeners.Contains( listener ) )
            {
                listeners.Add( listener );
                foreach ( int eventType in listener.RegisteredEvents )
                {
                    AddKnownEvent( eventType );
                }
            }
        }

        public void RemoveListener( ListenerBase listener)
        {
            if ( !listeners.Contains( listener ) )
            {
                listeners.Remove( listener );
            }
        }

        public void QueueEvent( EventBase @event )
        {
            if ( ValidateEvent( @event.EventType ) )
            {
                @event.CreationTime = DateTime.Now.Ticks;
                queuedEvents.Add( @event );
            }
        }

        public void QueueUniqueEvent( EventBase @event )
        {
            if ( ValidateEvent( @event.EventType ) )
            {
                for ( int x = queuedEvents.Count - 1 ; x >= 0 ; x-- )
                {
                    if ( queuedEvents[ x ].EventType == @event.EventType )
                    {
                        queuedEvents.RemoveAt( x );
                    }
                }
                queuedEvents.Add( @event );
            }
        }

        public void RemoveQueuedEvent( EventBase @event, bool allOccurances )
        {
            if ( queuedEvents.Contains( @event ) )
            {
                if ( allOccurances )
                {
                    for ( int x = queuedEvents.Count ; x >= 0 ; x-- )
                    {
                        queuedEvents.RemoveAt( x );
                    }
                }
                else
                {
                    queuedEvents.Remove( @event );
                }
            }
        }

        public void TriggerEvent( EventBase @event )
        {
            if ( ValidateEvent( @event.EventType ) )
            {
                foreach ( var listener in listeners.Where( listener => listener.RegisteredEvents.Contains( @event.EventType ) ).ToList() )
                {
                    listener.HandleEvent( @event );
                }
            }
        }

        private void AddKnownEvent( int eventType )
        {
            if ( !knownEvents.Contains( eventType ) )
            {
                knownEvents.Add( eventType );
            }
        }

        #endregion Operations

        #region Update

        public override void Update( GameTime gameTime )
        {
            if ( queuedEvents.Count == 0 )
                return;

            var startTime = DateTime.Now.Ticks;

            //Max of 5 milliseconds of processing time. 1ms = 10,000ticks
            var stopTime = startTime + 50000;

            //Copy the queued events to the active events list
            activeEvents = new List<EventBase>( queuedEvents );
            queuedEvents.Clear();

            //Process at least one event..or so Mr. McShaffry says
            var counter = activeEvents.Count - 1;
            do
            {
                if ( counter < 0 )
                {
                    break;
                }
                var currentTime = DateTime.Now.Ticks;
                var @event = activeEvents[ counter ];

                @event.HandleAttempts++;

                for ( var x = listeners.Count - 1 ; x >= 0 ; x-- )
                {
                    if ( listeners[ x ].RegisteredEvents.Contains( @event.EventType ) &&
                        listeners[ x ].HandleEvent( @event ) )
                    {
                        @event.Handled = true;
                    }
                }

                //If an event has been around for longer than 3 seconds, remove it
                //should change to 2 attempts?
                if ( currentTime >= @event.CreationTime + SecondsToTicks( @event.LifeTime ) )
                {
                    @event.Handled = true;
                }

                if ( @event.Handled )
                {
                    activeEvents.Remove( @event );
                    @event = null;
                }

                counter--;

                if ( DateTime.Now.Ticks >= stopTime )
                {
                    break;
                }
            } while ( activeEvents.Count > 0 );

            //Add back any unhandled events
            if ( activeEvents.Count > 0 )
            {
                queuedEvents.AddRange( activeEvents );
            }
        }

        #endregion Update

        #region Helper

        private long SecondsToTicks( int second )
        {
            return second * TimeSpan.TicksPerSecond;
        }

        #endregion Helper
    }
}