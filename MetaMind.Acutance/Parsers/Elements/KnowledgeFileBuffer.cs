namespace MetaMind.Acutance.Parsers.Elements
{
    using System.Collections.Generic;

    public class KnowledgeFileBuffer
    {
        public KnowledgeFileBuffer(KnowledgeFile file)
        {
            this.File  = file;
            this.Links = new List<KnowledgeLink>();
        }

        public KnowledgeFile File { get; private set; }

        public List<KnowledgeLink> Links { get; private set; }

        public void AddLink(KnowledgeLink link)
        {
            this.Links.Add(link);
        }
    }
}