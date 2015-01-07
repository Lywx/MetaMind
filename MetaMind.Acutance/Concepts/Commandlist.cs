namespace MetaMind.Acutance.Concepts
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;

    public enum CommandSortMode
    {
        Name,
    }

    public interface ICommandlist
    {
        [DataMember]
        List<CommandEntry> Commands { get; }

        void Add(CommandEntry entry);

        void Add(List<CommandEntry> entries);

        void Remove(CommandEntry entry);

        void Sort(CommandSortMode sortMode);

        void Update();
    }

    [DataContract]
    public class Commandlist : ICommandlist
    {
        #region Constructors

        public Commandlist()
        {
            this.Commands = new List<CommandEntry>();
        }

        #endregion Constructors

        #region Serialization

        [OnSerializing]
        public void OnSerializing(StreamingContext context)
        {
            // won't serialize non-from-knowledge
            this.Commands = CommandEntryFileter.FilterOutKnowledge(this.Commands);
        }

        #endregion Serialization

        #region Public Properties

        [DataMember]
        public List<CommandEntry> Commands { get; private set; }


        #endregion

        #region Operations 

        public void Add(List<CommandEntry> entries)
        {
            this.Commands.AddRange(entries);
        }

        public void Add(CommandEntry entry)
        {
            this.Commands.Add(entry);
        }

        public void Remove(CommandEntry entry)
        {
            if (this.Commands.Contains(entry))
            {
                this.Commands.Remove(entry);
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


        #endregion

        #region Update

        /// <summary>
        /// Update every instance of commmand entries.
        /// </summary>
        public void Update()
        {
            foreach (var command in this.Commands.ToArray())
            {
                command.Update();
            }
        }

        #endregion
    }
}