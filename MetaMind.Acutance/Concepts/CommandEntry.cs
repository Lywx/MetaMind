namespace MetaMind.Acutance.Concepts
{
    using System;
    using System.Runtime.Serialization;

    using MetaMind.Acutance.Events;
    using MetaMind.Acutance.Sessions;
    using MetaMind.Engine;
    using MetaMind.Engine.Components.Events;
    using MetaMind.Engine.Concepts;

    public enum CommandState
    {
        Running,
        Transiting,
    }

    public enum CommandType
    {
        Knowledge,
        Schedule,
    }

    public interface ICommandEntry
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
    public class CommandEntry : EngineObject, ICommandEntry
    {
        public CommandEntry(string name, string path, int offset, DateTime date, CommandRepeativity repeativity)
            : this(name, path, offset, CommandType.Schedule)
        {
            this.Timer = new CommandTimerWithDate(date, repeativity);

            this.Reset();
        }

        public CommandEntry(string name, string path, int offset, TimeSpan timeout, CommandRepeativity repeativity)
            : this(name, path, offset, CommandType.Knowledge)
        {
            this.Timer = new CommandTimerWithTimeout(timeout, repeativity);

            this.Reset();
        }

        private CommandEntry(string name, string path, int offset, CommandType type)
        {
            this.Name   = name;
            this.Path   = path;
            this.Offset = offset;
            this.Type   = type;
        }

        public Experience Experience
        {
            get { return this.Timer.Experience; }
        }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Path { get; set; }

        [DataMember]
        public int Offset { get; set; }

        [DataMember]
        public CommandState State { get; private set; }

        [DataMember]
        public CommandType Type { get; private set; }

        [DataMember]
        protected CommandTimer Timer { get; set; }

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
                (int)SessionEventType.CommandNotified,
                new CommandNotifiedEventArgs(this));

            GameEngine.EventManager.QueueEvent(commandNotifiedEvent);

            var knowledgeRetrievedEvent = new EventBase(
                (int)SessionEventType.KnowledgeRetrieved,
                new KnowledgeRetrievedEventArgs(this.Path, this.Offset));

            GameEngine.EventManager.QueueEvent(knowledgeRetrievedEvent);
        }

        private void UpdateState()
        {
            switch (this.State)
            {
                case CommandState.Running:
                    {
                        if (this.Timer.IsTransiting)
                        {
                            this.Transit();
                        }
                    }

                    break;

                case CommandState.Transiting:
                    {
                        if (this.Timer.IsAutoReseting)
                        {
                            this.Reset();
                        }
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