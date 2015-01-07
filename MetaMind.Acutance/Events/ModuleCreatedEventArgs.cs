namespace MetaMind.Acutance.Events
{
    using System;

    using MetaMind.Acutance.Parsers.Elements;
    using MetaMind.Engine;

    // TODO: not set yet
    public class ModuleCreatedEventArgs : EventArgs
    {
        public readonly KnowledgeFile File;

        // TODO : use module Entry?
        public ModuleCreatedEventArgs(KnowledgeFile file)
        {
            this.File = file;
        }
    }
}
