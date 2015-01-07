namespace MetaMind.Acutance.Events
{
    using System;

    using MetaMind.Acutance.Parsers.Elements;

    public class ModuleCreatedEventArgs : EventArgs
    {
        public readonly KnowledgeFile File;

        public ModuleCreatedEventArgs(KnowledgeFile file)
        {
            this.File = file;
        }

    }
}
