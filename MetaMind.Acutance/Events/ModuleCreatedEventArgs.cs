namespace MetaMind.Acutance.Events
{
    using System;

    using MetaMind.Acutance.Parsers.Elements;

    public class ModuleCreatedEventArgs : EventArgs
    {
        private readonly RawKnowledgeFileBuffer buffer;

        public ModuleCreatedEventArgs(RawKnowledgeFileBuffer buffer)
        {
            this.buffer = buffer;
        }

        public RawKnowledgeFileBuffer Buffer
        {
            get
            {
                return this.buffer;
            }
        }
    }
}
