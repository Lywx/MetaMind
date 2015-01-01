namespace MetaMind.Acutance.Concepts
{
    public class KnowledgeEntry
    {
        public KnowledgeEntry(string name, string commandName, string path, int minutes)
        {
            this.Name = name;
            this.Path = path;

            //this.Minutes = minutes;

            //this.Content = new KnowledgeContent(name, commandMode);
            //this.CommandName = commandName;

            //this.IsCommand = this.Minutes > 0;

            this.IsControl = false;
        }

        public KnowledgeEntry(string name)
        {
            this.Name = name;

            this.IsControl = true;
        }

        public string Name { get; set; }

        public string Path { get; private set; }

        public KnowledgeContent Content { get; private set; }

        public bool IsBlank { get; set; }

        public bool IsCommand { get; set; }

        public bool IsControl { get; set; }

        public bool IsFile { get; set; }

        public bool IsSearchResult { get; set; }
    }
}