namespace MetaMind.Acutance.Concepts
{
    public class KnowledgeEntry
    {
        public KnowledgeEntry(string name, string path, int minutes)
        {
            this.Name    = name;
            this.Path    = path;
            this.Minutes = minutes;

            this.IsControl = false;
            this.IsCall   = this.Minutes > 0;
        }

        public KnowledgeEntry(string name)
        {
            this.Name = name;

            this.IsControl = true;
        }

        public bool IsBlank { get; set; }

        public bool IsCall { get; set; }

        public bool IsControl { get; set; }

        public bool IsFile { get; set; }

        public bool IsSearchResult { get; set; }

        /// <summary>
        /// Timeout minutes for call entry creation.
        /// </summary>
        public int Minutes { get; set; }

        public string Name { get; set; }

        public string Path { get; private set; }
    }
}
