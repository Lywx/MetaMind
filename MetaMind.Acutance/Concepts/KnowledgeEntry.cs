namespace MetaMind.Acutance.Concepts
{
    public class KnowledgeEntry
    {
        public KnowledgeEntry(string name, string callName, string path, int minutes)
        {
            this.Name     = name;
            this.Path     = path;
            this.Minutes  = minutes;

            this.CallName = callName;
            this.IsCall   = this.Minutes > 0;

            this.IsControl = false;
        }

        public KnowledgeEntry(string name)
        {
            this.Name = name;

            this.IsControl = true;
        }

        public string CallName { get; set; }

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