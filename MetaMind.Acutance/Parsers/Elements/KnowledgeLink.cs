namespace MetaMind.Acutance.Parsers.Elements
{
    public class KnowledgeLink
    {
        public KnowledgeLink(string name)
        {
            this.Name = name;
        }

        public string Name { get; private set; }
    }
}