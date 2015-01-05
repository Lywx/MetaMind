namespace MetaMind.Acutance.Concepts
{
    using System.Linq;

    using MetaMind.Acutance.Parsers.Elements;

    public class KnowledgeEntry
    {
        public KnowledgeEntry(Knowledge knowledge)
        {
            if (knowledge != null)
            {
                this.Name = string.Concat(Enumerable.Repeat("#", (int)knowledge.Title.Level)) + " " + knowledge.Title.Name;
                this.Knowledge = knowledge;

                this.IsHighlighted = true;
            }
        }

        public KnowledgeEntry(string name, bool isControl)
        {
            this.Name      = name;

            this.IsControl = isControl;
        }

        public Knowledge Knowledge { get; private set; }

        public bool IsHighlighted { get; set; }

        public bool IsBlank { get; set; }

        public bool IsControl { get; set; }

        public bool IsFile { get; set; }

        public bool IsSearchResult { get; set; }

        public string Name { get; set; }
    }
}