namespace MetaMind.Acutance.Concepts
{
    using MetaMind.Acutance.Parsers.Elements;
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using IO = System.IO;

    [DataContract,
    KnownType(typeof(Commandlist))]
    public class ModuleEntry : TraceEntry
    {
        #region Constructors and Destructors

        public ModuleEntry(KnowledgeFile file, ICommandlist commandlist)
        {
            // convert to entries
            var entries = file.Knowledges.ConvertAll(k => k.ToKnowledgeEntry());

            this.Name = IO.Path.GetFileName(file.Path);
            this.Path = file.Path;

            this.ParentModule = null;
            this.SubModules = new List<ModuleEntry>();

            this.SubCommands = entries.ConvertAll(k => k.ToCommmandEntry());

            this.Commandlist = commandlist;
            this.Commandlist.Add(this.SubCommands);

            this.InitializeFileWatcher();
        }

        ~ModuleEntry()
        {
            this.Dispose();
        }

        #endregion Constructors and Destructors

        #region Disposal

        public override void Dispose()
        {
            // sub-elems
            if (this.SubCommands != null && this.Commandlist != null)
            {
                foreach (var subEntry in this.SubCommands.ToArray())
                {
                    this.Commandlist.Remove(subEntry);
                    this.SubCommands.Remove(subEntry);

                    subEntry.Dispose();
                }
            }

            if (this.SubModules != null)
            {
                foreach (var module in this.SubModules.ToArray())
                {
                    this.SubModules.Remove(module);

                    module.Dispose();
                }
            }

            // events
            this.Reloaded = null;

            // containers
            this.ParentModule = null;
            this.SubModules   = null;

            this.SubCommands = null;
            this.Commandlist = null;

            this.DisposeFileWatcher();
            base.Dispose();
        }

        private void DisposeFileWatcher()
        {
            this.Watcher.Changed -= this.Changed;

            this.Watcher.Dispose();
        }


        #endregion

        #region Serialization

        [OnDeserialized]
        public void InitializeFileWatcher(StreamingContext context)
        {
            this.InitializeFileWatcher();
        }

        #endregion

        #region Events

        public event EventHandler Reloaded;

        private void Changed(object sender, EventArgs e)
        {
            this.Changed();
        }

        private void Changed(object sender, IO.FileSystemEventArgs e)
        {
            this.Changed();
        }

        private void Changed()
        {
            if (this.Reloaded != null)
            {
                this.Reloaded(this, EventArgs.Empty);
            }
        }

        #endregion

        public bool IsPopulating
        {
            get
            {
                return this.SubCommands.Find(c => c.State != CommandState.Transiting) != null ||
                       this.SubModules.Find(m => m.IsPopulating) != null;
            }
        }

        [DataMember]
        public ModuleEntry ParentModule { get; set; }

        [DataMember]
        public string Path { get; set; }

        [DataMember]
        public List<CommandEntry> SubCommands { get; set; }

        [DataMember]
        public List<ModuleEntry> SubModules { get; set; }

        public IO.FileSystemWatcher Watcher { get; private set; }

        [DataMember]
        private ICommandlist Commandlist { get; set; }

        public void AddSubModule(ModuleEntry module)
        {
            this.SubModules.Add(module);

            module.ParentModule = this;
            module.Reloaded += this.Changed;
        }

        private void InitializeFileWatcher()
        {
            if (this.Watcher == null)
            {
                this.Watcher = new IO.FileSystemWatcher
                                   {
                                       NotifyFilter        = IO.NotifyFilters.LastWrite,
                                       Path                = IO.Path.GetDirectoryName(this.Path),
                                       Filter              = IO.Path.GetFileName(this.Path),
                                       EnableRaisingEvents = true,
                                   };

                this.Watcher.Changed += this.Changed;
            }
        }

        public override void Reset()
        {
            base.Reset();

            foreach (var subCommand in this.SubCommands)
            {
                subCommand.Reset();
            }

            foreach (var subModule in this.SubModules)
            {
                subModule.Reset();
            }
        }
    }
}