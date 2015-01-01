namespace MetaMind.Acutance.Concepts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;

    public enum CommandSortMode
    {
        Name, 
    }

    [DataContract]
    public class Commandlist
    {
        public Commandlist()
        {
            this.Commands = new List<CommandEntry>();
        }

        [DataMember]
        public List<CommandEntry> Commands { get; private set; }

        public CommandEntry Create(string name, string path, int minutes)
        {
            var entry = new CommandEntry(name, path, TimeSpan.FromMinutes(minutes));
            this.Commands.Add(entry);
            return entry;
        }

        public void Remove(CommandEntry entry)
        {
            if (this.Commands.Contains(entry))
            {
                this.Commands.Remove(entry);
            }
        }

        public void Update()
        {
            foreach (var @event in this.Commands.ToArray())
            {
                @event.Update();
            }
        }

        public void Sort(CommandSortMode sortMode)
        {
            switch (sortMode)
            {
                case CommandSortMode.Name:
                    {
                        this.Commands = this.Commands.OrderBy(command => command.Name).ToList();
                    }

                    break;
            }
        }
    }
}