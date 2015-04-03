namespace MetaMind.Acutance.Concepts
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    using MetaMind.Acutance.Parsers.Elements;

    using IO = System.IO;

    [DataContract,
    KnownType(typeof(Commandlist))]
    public class ModuleEntry : TraceEntry 
    {
        #region Constructors and Destructors

        public ModuleEntry(KnowledgeFile file, ICommandlist commandlist)
        {
            // convert to entries
            var knowledgeEntries = file.Knowledges.ConvertAll(k => k.ToKnowledgeEntry());

            this.Name = IO.Path.GetFileName(file.Path);
            this.Path = file.Path;

            this.ParentModuleEntry = null;
            this.SubModuleEntries  = new List<ModuleEntry>();

            this.SubCommandEntries = knowledgeEntries.ConvertAll(k => k.ToCommmandEntry());

            this.Commandlist = commandlist;
            this.Commandlist.Add(this.SubCommandEntries);
        }

        ~ModuleEntry()
        {
            this.Dispose();
        }

        public override void Dispose()
        {
            if (this.SubCommandEntries != null &&
                this.Commandlist != null)
            {
                foreach (var subEntry in this.SubCommandEntries.ToArray())
                {
                    this.Commandlist      .Remove(subEntry);
                    this.SubCommandEntries.Remove(subEntry);

                    subEntry.Dispose();
                }
            }

            if (this.SubModuleEntries != null)
            {
                foreach (var module in this.SubModuleEntries.ToArray())
                {
                    this.SubModuleEntries.Remove(module);

                    module.Dispose();
                }
            }

            this.Name = null;
            this.Path = null;

            this.ParentModuleEntry = null;
            this.SubModuleEntries  = null;

            this.SubCommandEntries = null;

            this.Commandlist = null;

            base.Dispose();
        }

        #endregion

        public bool IsPopulating
        {
            get
            {
                return this.SubCommandEntries.Find(c => c.State != CommandState.Transiting) != null || 
                       this.SubModuleEntries.Find(m => m.IsPopulating) != null;
            }
        }

        public override void Reset()
        {
            base.Reset();

            foreach (var subCommand in this.SubCommandEntries)
            {
                subCommand.Reset();
            }

            foreach (var subModule in this.SubModuleEntries)
            {
                subModule.Reset();
            }
        }

        [DataMember]
        public string Path { get; set; }

        [DataMember]
        public List<CommandEntry> SubCommandEntries { get; set; }

        [DataMember]
        public List<ModuleEntry> SubModuleEntries { get; set; }

        [DataMember]
        public ModuleEntry ParentModuleEntry { get; set; }

        [DataMember]
        private ICommandlist Commandlist { get; set; }

        public void AddSubModule(ModuleEntry module)
        {
            module.ParentModuleEntry = this;
            this.SubModuleEntries.Add(module);
        }
    }
}