namespace MetaMind.Acutance.Concepts
{
    public class KnowledgeEntry
    {
        public string Name;

        public int    Timeout;

        public bool   IsControl;

        public bool   IsFile;

        public bool   IsSearchResult;

        public bool   IsBlank;

        public KnowledgeEntry(string name, int timeout)
        {
            this.IsControl = false;

            this.Name    = name;
            this.Timeout = timeout;
        }

        public KnowledgeEntry(string name)
        {
            this.IsControl = true;

            this.Name = name;
        }
    }
}
