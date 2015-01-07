namespace MetaMind.Acutance.Parsers.Elements
{
    using MetaMind.Engine.Parsers.Elements;

    public class Title
    {
        private readonly TitleLevel level;
        private readonly string     name;
        private readonly TimeTag    tag;

        public Title(TitleLevel level, Sentence sentence)
            : this(level, sentence, TimeTag.Zero)
        {
        }

        public Title(TitleLevel level, Sentence sentence, TimeTag tag)
        {
            this.level = level;
            this.name  = sentence.ToString();
            this.tag   = tag;
        }

        public string Name
        {
            get
            {
                return this.name;
            }
        }

        public TitleLevel Level
        {
            get { return this.level; }
        }

        public TimeTag Tag
        {
            get
            {
                return this.tag;
            }
        }
    }
}