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
                this.Knowledge = knowledge;

                this.Name = string.Concat(Enumerable.Repeat("#", (int)knowledge.Title.Level)) + " " + knowledge.Title.Name;

                this.IsTitle = true;
            }
        }

        public KnowledgeEntry(string name, bool isControl)
        {
            this.Name      = name;

            this.IsControl = isControl;
        }

        public Knowledge Knowledge { get; private set; }

        public bool IsTitle { get; set; }

        public bool IsBlank { get; set; }

        public bool IsControl { get; set; }

        public bool IsFile { get; set; }

        public bool IsResult { get; set; }

        public string Name { get; set; }

        //public CommandEntry ToCommmandEntry()
        //{
        //    // TODO: how to convert to command entry easier
        //    return Acutance.Session.Commandlist.Create(CommandType.Knowledge, )
        //}
    }
}