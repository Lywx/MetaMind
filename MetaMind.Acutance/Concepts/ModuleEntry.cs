namespace MetaMind.Acutance.Concepts
{
    using System.Collections.Generic;

    using MetaMind.Acutance.Parsers.Elements;

    public class ModuleEntry : TraceEntry
    {
        public ModuleEntry(KnowledgeFile file)
        {
            this.Name       = file.Path;
            //this.SubEntries = file.Knowledges.ConvertAll(k => k.T);

            //TODO: make to command list
        }

        public void Remove()
        {
            //this.SubEntries.F// TODO: remove from commmand list
        }

        public List<CommandEntry> SubEntries { get; set; }
    }
}