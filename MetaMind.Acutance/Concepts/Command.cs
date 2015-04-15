namespace MetaMind.Acutance.Concepts
{
    using System;
    using System.Runtime.Serialization;

    using MetaMind.Acutance.Events;
    using MetaMind.Acutance.Sessions;
    using MetaMind.Engine;
    using MetaMind.Engine.Components.Events;
    using MetaMind.Engine.Concepts;

    public interface ICommand
    {
        [DataMember]
        SynchronizationSpan SynchronizationSpan { get; }

        [DataMember]
        string Name { get; set; }

        [DataMember]
        string Path { get; set; }

        void Reset();
    }

    [DataContract]
    public class Command : ICommand, IDisposable
    {
        public Command(string name, string path, int offset, DateTime date, CommandRepetion repetion)
            : this(name, path, offset, CommandType.Schedule)
        {
            this.Timer = new CommandTimerWithDate(date, repetion);

            this.Reset();
        }

        public Command(string name, string path, int offset, TimeSpan timeout, CommandRepetion repetion)
            : this(name, path, offset, CommandType.Knowledge)
        {
            this.Timer = new CommandTimerWithTimeout(timeout, repetion);

            this.Reset();
        }

        private Command(string name, string path, int offset, CommandType type)
        {
            this.Name   = name;
            this.Path   = path;
            this.Offset = offset;
            this.Type   = type;
        }

        ~Command()
        {
            this.Dispose();
        }

        public void Dispose()
        {
            this.Timer.Dispose();

            this.State = CommandState.Terminated;
        }

        public SynchronizationSpan SynchronizationSpan
        {
            get { return this.Timer.SynchronizationSpan; }
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

            var commandNotifiedEvent = new Event(
                (int)SessionEventType.CommandNotified,
                new CommandNotifiedEventArgs(this));

            GameEngine.Events.QueueEvent(commandNotifiedEvent);

            var knowledgeRetrievedEvent = new Event(
                (int)SessionEventType.KnowledgeRetrieved,
                new KnowledgeRetrievedEventArgs(this.Path, this.Offset));

            GameEngine.Events.QueueEvent(knowledgeRetrievedEvent);
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