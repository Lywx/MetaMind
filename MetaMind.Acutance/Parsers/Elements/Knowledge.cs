namespace MetaMind.Acutance.Parsers.Elements
{
    using System;

    public class Knowledge
    {
        public Knowledge(Title title, KnowledgeFile file, int position)
        {
            if (file == null)
            {
                throw new ArgumentNullException("file");
            }

            if (position < 0)
            {
                throw new ArgumentOutOfRangeException("position");
            }

            this.Title    = title;
            this.File   = file;
            this.Position = position;

            this.File.Add(this);
        }

        public Title Title { get; private set; }

        public int Position { get; private set; }

        public KnowledgeFile File { get; set; }
    }
}