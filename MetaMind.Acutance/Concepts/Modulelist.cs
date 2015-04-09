namespace MetaMind.Acutance.Concepts
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;

    using FileSearcher;

    using MetaMind.Acutance.Parsers.Elements;
    using MetaMind.Acutance.Settings;

    public enum ModuleSortMode
    {
        Name,
    }

    public interface IModulelist
    {
        [DataMember]
        List<Module> Modules { get; }

        Module Create(RawKnowledgeFile file);

        void Remove(Module entry);

        void Sort(ModuleSortMode sortMode);

        void Update();
    }

    [DataContract,
    KnownType(typeof(Commandlist))]
    public class Modulelist : IModulelist
    {
        public Modulelist(ICommandlist commandlist)
        {
            this.Commandlist = commandlist;

            this.Modules     = new List<Module>();
        }

        [DataMember]
        public List<Module> Modules { get; private set; }

        [DataMember]
        private ICommandlist Commandlist { get; set; }

        #region Operations

        public Module Create(RawKnowledgeFile file)
        {
            var entry = new Module(file, this.Commandlist);
            this.Modules.Add(entry);
            return entry;
        }

        public void Remove(Module entry)
        {
            if (this.Modules.Contains(entry))
            {
                this.Modules.Remove(entry);
            }
        }

        public void Sort(ModuleSortMode sortMode)
        {
            switch (sortMode)
            {
                case ModuleSortMode.Name:
                    {
                        this.Modules = this.Modules.OrderBy(m => m.Name).ToList();
                    }

                    break;
            }
        }

        #endregion

        #region Update

        public void Update()
        {
            foreach (var trace in this.Modules.ToArray())
            {
                trace.Update();
            }
        }

        #endregion
    }
}