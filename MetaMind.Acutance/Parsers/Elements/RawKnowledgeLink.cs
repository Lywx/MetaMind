namespace MetaMind.Acutance.Parsers.Elements
{
    public class RawKnowledgeLink
    {
        public RawKnowledgeLink(string name)
        {
            this.Name = name;
        }

        public string Name { get; private set; }
    }
}