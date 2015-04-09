namespace MetaMind.Acutance.Concepts
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    using MetaMind.Acutance.Parsers.Elements;

    using IO = System.IO;

    [DataContract,
    KnownType(typeof(Commandlist))]
    public class Module : Trace
    {
        #region Constructors

        public Module(RawKnowledgeFile file, ICommandlist commandlist)
        {
            this.Name = IO.Path.GetFileName(file.Path);
            this.Path = file.Path;

            this.Parent = null;
            this.Minions = new List<Module>();

            this.InitializeCommands(file, commandlist);
            this.InitializeFileWatcher();
        }

        #endregion Constructors

        #region Destructors

        ~Module()
        {
            this.Dispose();
        }

        #endregion Destructors

        #region Disposal

        public override void Dispose()
        {
            // sub-elems
            if (this.Commands != null && this.Commandlist != null)
            {
                foreach (var subEntry in this.Commands.ToArray())
                {
                    this.Commandlist.Remove(subEntry);
                    this.Commands.Remove(subEntry);

                    subEntry.Dispose();
                }
            }

            if (this.Minions != null)
            {
                foreach (var module in this.Minions.ToArray())
                {
                    this.Minions.Remove(module);

                    module.Dispose();
                }
            }

            // events
            this.Reloaded = null;

            // containers
            this.Parent = null;
            this.Minions = null;

            this.Commands = null;
            this.Commandlist = null;

            this.DisposeFileWatcher();
            base.Dispose();
        }

        private void DisposeFileWatcher()
        {
            this.Watcher.Changed -= this.Changed;

            this.Watcher.Dispose();
        }

        #endregion Disposal

        #region Serialization

        [OnDeserialized]
        public void InitializeFileWatcher(StreamingContext context)
        {
            this.InitializeFileWatcher();
        }

        #endregion Serialization

        #region Initialization

        private void InitializeCommands(RawKnowledgeFile file, ICommandlist commandlist)
        {
            var knowledges = file.Knowledges.ConvertAll(rk => rk.ToKnowledge());
            this.Commands = knowledges.ConvertAll(k => k.ToCommmand());

            this.Commandlist = commandlist;
            this.Commandlist.Add(this.Commands);
        }

        private void InitializeFileWatcher()
        {
            if (this.Watcher == null)
            {
                this.Watcher = new IO.FileSystemWatcher
                                   {
                                       NotifyFilter = IO.NotifyFilters.LastWrite,
                                       Path = IO.Path.GetDirectoryName(this.Path),
                                       Filter = IO.Path.GetFileName(this.Path),
                                       EnableRaisingEvents = true,
                                   };

                this.Watcher.Changed += this.Changed;
            }
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

        #endregion Events

        #region File Data

        [DataMember]
        public string Path { get; set; }

        public IO.FileSystemWatcher Watcher { get; private set; }

        #endregion

        #region State Data

        public bool IsPopulating
        {
            get
            {
                return this.Commands.Find(c => c.State != CommandState.Transiting) != null
                       || this.Minions.Find(m => m.IsPopulating) != null;
            }
        }

        #endregion

        #region Command Data

        [DataMember]
        public List<Command> Commands { get; set; }

        [DataMember]
        private ICommandlist Commandlist { get; set; }

        #endregion

        #region Relation Data

        [DataMember]
        public Module Parent { get; set; }

        [DataMember]
        public List<Module> Minions { get; set; }

        #endregion

        #region Operations

        public void Add(Module module)
        {
            this.Minions.Add(module);

            module.Parent = this;
            module.Reloaded += this.Changed;
        }

        public override void Reset()
        {
            base.Reset();

            foreach (var command in this.Commands)
            {
                command.Reset();
            }

            foreach (var module in this.Minions)
            {
                module.Reset();
            }
        }

        #endregion
    }
}