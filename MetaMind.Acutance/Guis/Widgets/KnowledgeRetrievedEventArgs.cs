namespace MetaMind.Acutance.Guis.Widgets
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