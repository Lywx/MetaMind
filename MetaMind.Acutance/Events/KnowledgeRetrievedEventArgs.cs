namespace MetaMind.Acutance.Events
{
    using System;

    public class KnowledgeRetrievedEventArgs : EventArgs
    {
        private readonly int offset;
        private readonly string path;

        public KnowledgeRetrievedEventArgs(string path, int offset)
        {
            this.path = path;
            this.offset = offset;
        }

        public int Offset
        {
            get
            {
                return this.offset;
            }
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