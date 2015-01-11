namespace MetaMind.Acutance.Parsers.Elements
{
    using MetaMind.Engine.Parsers.Elements;

    public class Title
    {
        private readonly TitleLevel level;
        private readonly RepeativityTag repeativity;
        private readonly string     name;
        private readonly TimeTag    time;

        public Title(TitleLevel level, RepeativityTag repeativity, Sentence sentence)
            : this(level, repeativity, sentence, TimeTag.Zero)
        {
        }

        public Title(TitleLevel level, RepeativityTag repeativity, Sentence sentence, TimeTag time)
        {
            this.level       = level;
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