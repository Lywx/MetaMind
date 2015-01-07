namespace MetaMind.Acutance.Concepts
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    using MetaMind.Acutance.Parsers.Elements;

    [DataContract,
    KnownType(typeof(Commandlist))]
    public class ModuleEntry : TraceEntry 
    {
        #region Constructors and Destructors

        public ModuleEntry(KnowledgeFile file, ICommandlist commandlist)
        {
            // convert to entries
            var knowledgeEntries = file.Knowledges.ConvertAll(k => k.ToKnowledgeEntry());

            this.Name = System.IO.Path.GetFileName(file.Path);
            this.Path = file.Path;
            this.Commandlist = commandlist;
            this.SubEntries = knowledgeEntries.ConvertAll(k => k.ToCommmandEntry());

            this.Commandlist.Add(this.SubEntries);
        }

        ~ModuleEntry()
        {
            this.Dispose();
        }

        public override void Dispose()
        {
            if (this.SubEntries != null &&
                this.Commandlist != null)
            {
                foreach (var subEntry in this.SubEntries.ToArray())
                {
                    this.Commandlist.Remove(subEntry);
                    this.SubEntries.Remove(subEntry);
                }
            }

            this.Name = null;
            this.Path = null;
            this.Commandlist = null;
            this.SubEntries = null;

            base.Dispose();
        }

        #endregion

        public bool Populating
        {
            get
            {
                return this.SubEntries.Find(c => c.State != CommandState.Transiting) != null;
            }
        }

        public override void Reset()
        {
            base.Reset();

            foreach (var subEntry in this.SubEntries)
            {
                subEntry.Reset();
            }
        }

        [DataMember]
        public string Path { get; set; }

        [DataMember]
        public List<CommandEntry> SubEntries { get; set; }

        [DataMember]
        private ICommandlist Commandlist { get; set; }
    }
}