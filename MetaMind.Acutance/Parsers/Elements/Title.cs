namespace MetaMind.Acutance.Parsers.Elements
{
    using MetaMind.Engine.Parsers.Elements;

    public class Title
    {
        private readonly TitleLevel     level;
        private readonly TitleType      type;
        private readonly string         name;
        private readonly RepetitionTag  repetition;
        private readonly TimeTag        time;

        public Title(TitleLevel level, TitleType type, RepetitionTag repetition, Sentence sentence)
            : this(level, type, repetition, sentence, TimeTag.Zero)
        {
        }

        public Title(TitleLevel level, TitleType type, RepetitionTag repetition, Sentence sentence, TimeTag time)
        {
            this.level       = level;
            this.type        = type;
            this.repetition  = repetition;
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

        public RepetitionTag Repetition
        {
            get { return this.repetition; }
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