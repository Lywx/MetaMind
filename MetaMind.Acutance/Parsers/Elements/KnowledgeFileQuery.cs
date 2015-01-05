namespace MetaMind.Acutance.Parsers.Elements
{
    using System.Collections.Generic;

    using MetaMind.Acutance.Concepts;

    public class KnowledgeFileQuery
    {
        public KnowledgeFileQuery(KnowledgeFile file)
        {
            this.Entries = new List<KnowledgeEntry>();

            this.File = file;
        }

        public List<KnowledgeEntry> Entries { get; private set; }

        public KnowledgeFile File { get; private set; }

        public void AddEntry(KnowledgeEntry entry)
        {
            this.Entries.Add(entry);
        }
    }
}