namespace MetaMind.Acutance.Events
{
    using System;

    using MetaMind.Acutance.Parsers.Elements;

    public class ModuleCreatedEventArgs : EventArgs
    {
        public readonly RawKnowledgeFileBuffer Buffer;

        public ModuleCreatedEventArgs(RawKnowledgeFileBuffer buffer)
        {
            this.Buffer = buffer;
        }
    }
}
