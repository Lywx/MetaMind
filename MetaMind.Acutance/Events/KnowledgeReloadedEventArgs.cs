namespace MetaMind.Acutance.Events
{
    using System;

    public class KnowledgeReloadedEventArgs : EventArgs
    {
        public readonly string Path;

        public KnowledgeReloadedEventArgs(string path)
        {
            this.Path = path;
        }
    }
}