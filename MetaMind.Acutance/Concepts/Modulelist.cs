namespace MetaMind.Acutance.Concepts
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;

    using MetaMind.Acutance.Parsers.Elements;

    public enum ModuleSortMode
    {
        Name,
    }

    public interface IModulelist
    {
        [DataMember]
        List<ModuleEntry> Modules { get; }

        ModuleEntry Create(KnowledgeFile file);

        void Remove(ModuleEntry entry);

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

            this.Modules = new List<ModuleEntry>();
        }

        [DataMember]
        public List<ModuleEntry> Modules { get; private set; }

        [DataMember]
        private ICommandlist Commandlist { get; set; }

        #region Operations

        public ModuleEntry Create(KnowledgeFile file)
        {
            var entry = new ModuleEntry(file, this.Commandlist);
            this.Modules.Add(entry);
            return entry;
        }

        public void Remove(ModuleEntry entry)
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