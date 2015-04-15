namespace MetaMind.Acutance.Events
{
    using System;

    public class KnowledgeReloadedEventArgs : EventArgs
    {
        private readonly string path;

        public KnowledgeReloadedEventArgs(string path)
        {
            this.path = path;
        }

        public string Path
        {
            get
            {
                return this.path;
            }
        }
    }
}