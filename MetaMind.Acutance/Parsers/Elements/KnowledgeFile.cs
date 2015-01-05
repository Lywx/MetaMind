namespace MetaMind.Acutance.Parsers.Elements
{
    using System.Collections.Generic;

    public class KnowledgeFile
    {
        public KnowledgeFile(string path)
        {
            this.Knowledges = new List<Knowledge>();

            this.Path = path;
        }

        public List<Knowledge> Knowledges { get; private set; }

        public string Path { get; private set; }

        public void Add(Knowledge knowledge)
        {
            this.Knowledges.Add(knowledge);
        }
    }
}