namespace MetaMind.Acutance.Events
{
    using System;

    using MetaMind.Acutance.Parsers.Elements;

    public class ModuleCreatedEventArgs : EventArgs
    {
        public readonly KnowledgeFileBuffer Buffer;

        public ModuleCreatedEventArgs(KnowledgeFileBuffer buffer)
        {
            this.Buffer = buffer;
        }
    }
}
