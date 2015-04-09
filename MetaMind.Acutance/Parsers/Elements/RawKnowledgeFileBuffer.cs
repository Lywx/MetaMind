namespace MetaMind.Acutance.Parsers.Elements
{
    using System.Collections.Generic;

    public class RawKnowledgeFileBuffer
    {
        public RawKnowledgeFileBuffer(RawKnowledgeFile file)
        {
            this.File  = file;
            this.Links = new List<RawKnowledgeLink>();
        }

        public RawKnowledgeFile File { get; private set; }

        public List<RawKnowledgeLink> Links { get; private set; }

        public void AddLink(RawKnowledgeLink link)
        {
            this.Links.Add(link);
        }
    }
}