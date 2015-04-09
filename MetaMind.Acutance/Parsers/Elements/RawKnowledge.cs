namespace MetaMind.Acutance.Parsers.Elements
{
    using System;

    using MetaMind.Acutance.Concepts;

    public class RawKnowledge
    {
        public RawKnowledge(Title title, string path, int offset)
        {
            if (path == null)
            {
                throw new ArgumentNullException("path");
            }

            if (offset < 0)
            {
                throw new ArgumentOutOfRangeException("offset");
            }

            this.Title  = title;
            this.Path   = path;
            this.Offset = offset;
        }

        public string Path { get; private set; }

        public Title Title { get; private set; }

        public int Offset { get; private set; }

        public Knowledge ToKnowledge()
        {
            return new Knowledge(this);
        }
    }
}