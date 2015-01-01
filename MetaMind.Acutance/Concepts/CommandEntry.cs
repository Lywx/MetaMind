namespace MetaMind.Acutance.Concepts
{
    using System;
    using System.Runtime.Serialization;

    using MetaMind.Acutance.Guis.Widgets;
    using MetaMind.Acutance.Sessions;
    using MetaMind.Engine;
    using MetaMind.Engine.Components.Events;
    using MetaMind.Engine.Concepts;

    public enum CommandMode
    {
        TriggeredByTimeout,
        TriggeredByDate,
    }

    public enum CommandState
    {
        Running,
        Transiting,
    }

    public interface IEventEntry
    {
        [DataMember]
        Experience Experience { get; }

        [DataMember]
        string Name { get; set; }

        [DataMember]
        string Path { get; set; }

        void Reset();
    }

    [DataContract]
    public class CommandEntry : EngineObject, IEventEntry
    {
        public CommandEntry(string name, string path, DateTime date, CommandRepeativity repeativity)
            : this(name, path, CommandMode.TriggeredByDate)
        {
            this.Timer = new CommandTimerByDate(date, repeativity);

            this.Reset();
        }

        public CommandEntry(string name, string path, TimeSpan timeout)
            : this(name, path, CommandMode.TriggeredByTimeout)
        {
            this.Timer = new CommandTimerByTimeout(timeout);

            this.Reset();
        }

        private CommandEntry(string name, string path, CommandMode mode)
        {
            this.Name = name;
            this.Path = path;
            this.Mode = mode;
        }

        [DataMember]
        public Experience Experience
        {
            get { return this.Timer.Experience; }
        }

        [DataMember]
        public CommandMode Mode { get; private set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Path { get; set; }

        [DataMember]
        public CommandState State { get; private set; }

        [DataMember]
        private CommandTimer Timer { get; set; }

        public void Check()
        {
            if (this.State != CommandState.Transiting)
            {
                return;
            }

            this.Reset();
        }

        public void Reset()
        {
            this.State = CommandState.Running;

            this.Timer.Reset();
        }

        public void Update()
        {
            this.UpdateTimer();
            this.UpdateState();
        }

        private void Transit()
        {
            this.State = CommandState.Transiting;

            var commandNotifiedEvent = new EventBase(
                (int)AdventureEventType.CommandNotified,
                new CommandNotifiedEventArgs(this));

            GameEngine.EventManager.QueueEvent(commandNotifiedEvent);

            var knowledgeRetrievedEvent = new EventBase(
                (int)AdventureEventType.KnowledgeRetrieved,
                new KnowledgeRetrievedEventArgs(this.Path));

            GameEngine.EventManager.QueueEvent(knowledgeRetrievedEvent);
        }

        private void UpdateState()
        {
            switch (this.State)
            {
                case CommandState.Running:
                    {
                        if (this.Timer.Transiting)
                        {
                            this.Transit();
                        }
                    }

                    break;

                case CommandState.Transiting:
                    {
                    }

                    break;
            }
        }

        private void UpdateTimer()
        {
            this.Timer.Update();
        }
    }
}