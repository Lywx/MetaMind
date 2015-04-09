namespace MetaMind.Acutance.Parsers.Elements
{
    using System.Collections.Generic;

    public class RawKnowledgeFile
    {
        public RawKnowledgeFile(string path)
        {
            this.Knowledges = new List<RawKnowledge>();

            this.Path = path;
        }

        public List<RawKnowledge> Knowledges { get; private set; }

        public string Path { get; private set; }

        public void AddKnowledge(RawKnowledge rawKnowledge)
        {
            this.Knowledges.Add(rawKnowledge);
        }
    }
}