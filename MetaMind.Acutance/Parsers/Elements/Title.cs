namespace MetaMind.Acutance.Parsers.Elements
{
    using MetaMind.Engine.Parsers.Elements;

    public class Title
    {
        private readonly TitleLevel     level;
        private readonly TitleType      type;
        private readonly RepeativityTag repeativity;
        private readonly string         name;
        private readonly TimeTag        time;

        public Title(TitleLevel level, TitleType type, RepeativityTag repeativity, Sentence sentence)
            : this(level, type, repeativity, sentence, TimeTag.Zero)
        {
        }

        public Title(TitleLevel level, TitleType type, RepeativityTag repeativity, Sentence sentence, TimeTag time)
        {
            this.level       = level;
            this.type        = type;
            this.repeativity = repeativity;
            this.name        = sentence.ToString();
            this.time        = time;
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

        public TitleType Type
        {
            get { return this.type; }
        }

        public RepeativityTag Repeativity
        {
            get { return this.repeativity; }
        }

        public TimeTag Time
        {
            get
            {
                return this.time;
            }
        }
    }
}