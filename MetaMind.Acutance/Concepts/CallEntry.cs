namespace MetaMind.Acutance.Concepts
{
    using System;
    using System.Diagnostics;
    using System.Runtime.Serialization;

    using MetaMind.Acutance.Guis.Widgets;
    using MetaMind.Acutance.Sessions;
    using MetaMind.Engine;
    using MetaMind.Engine.Components.Events;
    using MetaMind.Engine.Concepts;

    public interface IEventEntry
    {
        [DataMember]
        Experience Experience { get; set; }

        [DataMember]
        string Name { get; set; }

        [DataMember]
        string Path { get; set; }

        void Reset();
    }

    [DataContract]
    public class CallEntry : EngineObject, IEventEntry
    {
        private Stopwatch timer;

        [DataMember]
        private TimeSpan transition = TimeSpan.FromSeconds(30);

        public CallEntry(string name, string path, TimeSpan timeout)
        {
            this.Name    = name;
            this.Path    = path;
            this.Timeout = timeout;

            this.State = EventState.Running;

            this.Reset();
            this.ResetTimer();
        }

        public enum EventState
        {
            Running, Transiting,
        }

        [DataMember]
        public Experience Experience { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Path { get; set; }

        [DataMember]
        public EventState State { get; private set; }

        [DataMember]
        public TimeSpan Timeout { get; private set; }

        [OnDeserialized]
        public void OnDeserialized(StreamingContext context)
        {
            this.ResetTimer();
        }

        public void Reset()
        {
            this.Experience = Experience.Zero;
        }

        public void Update()
        {
            this.UpdateTimer();
            this.UpdateState();
        }

        private void ResetTimer()
        {
            this.timer = new Stopwatch();
            this.timer.Start();
        }

        private void UpdateState()
        {
            switch (this.State)
            {
                case EventState.Running:
                    {
                        if (this.Experience.CertainDuration >= this.Timeout)
                        {
                            this.State = EventState.Transiting;

                            var callNotifiedEvent = new EventBase(
                                (int)AdventureEventType.CallNotified,
                                new CallNotifiedEventArgs());
                            GameEngine.EventManager.QueueEvent(callNotifiedEvent);

                            var knowledgeRetrievedEvent = new EventBase(
                                (int)AdventureEventType.KnowledgeRetrieved,
                                new KnowledgeRetrievedEventArgs(this.Path));

                            GameEngine.EventManager.QueueEvent(knowledgeRetrievedEvent);
                        }
                    }

                    break;

                case EventState.Transiting:
                    {
                        if (this.Experience.CertainDuration >= this.Timeout + this.transition)
                        {
                            this.State = EventState.Running;

                            this.Reset();
                        }
                    }

                    break;
            }
        }

        private void UpdateTimer()
        {
            this.Experience += this.timer.Elapsed;

            this.timer.Restart();
        }
    }
}