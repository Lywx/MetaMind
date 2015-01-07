namespace MetaMind.Acutance.Parsers.Elements
{
    using System;

    public class Knowledge
    {
        public Knowledge(Title title, KnowledgeFile file, int offset)
        {
            if (file == null)
            {
                throw new ArgumentNullException("file");
            }

            if (offset < 0)
            {
                throw new ArgumentOutOfRangeException("offset");
            }

            this.Title  = title;
            this.File   = file;
            this.Offset = offset;

            this.File.Add(this);
        }

        public Title Title { get; private set; }

        public int Offset { get; private set; }

        public KnowledgeFile File { get; set; }
    }
}