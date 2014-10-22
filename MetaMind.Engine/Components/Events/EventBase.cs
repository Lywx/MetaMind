namespace MetaMind.Engine.Components.Events
{
    public class EventBase
    {
        /// <summary>
        /// Base class for Game Events
        /// </summary>
        /// <param name="eventType">The type of event being sent</param>
        /// <param name="data">Any type of data that needs to go with the event.  Can be an object, a value, null, etc</param>
        /// <param name="lifeTime">The length in Seconds the event should stay alive if not picked up</param>
        public EventBase(int eventType, object data, int lifeTime = 1, int handleAttempts = 0)
        {
            this.eventType      = eventType;
            this.data           = data;
            this.lifeTime       = lifeTime;
            this.handleAttempts = handleAttempts;
        }

        private int    eventType;
        private object data;
        private long   creationTime;
        private int    lifeTime;
        private bool   handled;
        private int    handleAttempts;

        public int EventType
        {
            get { return eventType; }
            set { eventType = value; }
        }
        public object Data
        {
            get { return data; }
            set { data = value; }
        }
        public long CreationTime
        {
            get { return creationTime; }
            set { creationTime = value; }
        }
        public int LifeTime
        {
            get { return lifeTime; }
            set { lifeTime = value; }
        }
        public bool Handled
        {
            get { return handled; }
            set { handled = value; }
        }
        public int HandleAttempts
        {
            get { return handleAttempts; }
            set { handleAttempts = value; }
        }
    }
}