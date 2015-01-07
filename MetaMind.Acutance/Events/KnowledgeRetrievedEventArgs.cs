namespace MetaMind.Acutance.Events
{
    using System;

    public class KnowledgeRetrievedEventArgs : EventArgs
    {
        public readonly string Path;

        public readonly int Offset;

        public KnowledgeRetrievedEventArgs(string path, int offset)
        {
            this.Path   = path;
            this.Offset = offset;
        }
    }
}