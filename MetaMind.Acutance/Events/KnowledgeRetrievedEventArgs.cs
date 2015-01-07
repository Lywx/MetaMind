namespace MetaMind.Acutance.Events
{
    using System;

    public class KnowledgeRetrievedEventArgs : EventArgs
    {
        public readonly string Path;

        public KnowledgeRetrievedEventArgs(string path)
        {
            this.Path = path;
        }
    }
}