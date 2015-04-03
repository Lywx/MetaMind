namespace MetaMind.Acutance.Concepts
{
    using System.Collections.Generic;
    using System.IO;

    using FileSearcher;

    using MetaMind.Acutance.Guis.Widgets;
    using MetaMind.Acutance.Parsers.Elements;
    using MetaMind.Acutance.Settings;

    public class ModuleLinker
    {
        private static object locker = new object();

        public ModuleLinker(ModuleItemFactory moduleItemFactory, IModulelist modulelist)
        {
            this.ModuleItemFactory = moduleItemFactory;
            this.Modulelist        = modulelist;

            this.CompletionTable = new Dictionary<string, bool>();
        }

        private Dictionary<string, bool> CompletionTable { get; set; }

        private ModuleItemFactory ModuleItemFactory { get; set; }

        private IModulelist Modulelist { get; set; }

        private ModuleEntry Target { get; set; }

        public void Initialize(ModuleEntry module)
        {
            this.Target = module;

            this.CompletionTable = new Dictionary<string, bool>();
        }

        public void Prepare(KnowledgeLink link)
        {
            this.CompletionTable.Add(link.Name, false);
        }

        public void Start()
        {
            foreach (var pair in this.CompletionTable)
            {
                var searchName   = SearchSettings.SearchName(pair.Key, false);
                var searchParams = SearchSettings.SearchParams(searchName);

                var searcher = new Searcher();
                searcher.FoundInfo += this.Link;

                searcher.Start(searchParams);
            }
        }

        private void Link(FoundInfoEventArgs e)
        {
            var name = Path.GetFileNameWithoutExtension(e.Info.FullName);

            // note that lock has to be static
            lock (locker)
            {
                // only allow one sub-module even if they share a same name
                if (!this.CompletionTable[name])
                {
                    var query = KnowledgeLoader.LoadFile(e.Info.FullName, 0);

                    // recursive calling to create sub-module
                    var module = this.ModuleItemFactory.CreateData(query.Buffer);
                    this.Target.AddSubModule(module);
                }

                this.CompletionTable[name] = true;
            }
        }
    }
}