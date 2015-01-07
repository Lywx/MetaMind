namespace MetaMind.Acutance.Concepts
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    using MetaMind.Acutance.Parsers.Elements;

    public interface IModulelist
    {
        [DataMember]
        List<ModuleEntry> Modules { get; }

        ModuleEntry Create(KnowledgeFile file);

        void Remove(ModuleEntry entry);

        void Update();
    }

    [DataContract]
    public class Modulelist : IModulelist
    {
        public Modulelist()
        {
            this.Modules = new List<ModuleEntry>();
        }

        [DataMember]
        public List<ModuleEntry> Modules { get; private set; }

        public ModuleEntry Create(KnowledgeFile file)
        {
            var entry = new ModuleEntry(file);
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

        public void Update()
        {
            foreach (var trace in this.Modules.ToArray())
            {
                trace.Update();
            }
        }
    }
}