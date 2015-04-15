namespace MetaMind.Acutance.Concepts
{
    using System.Linq;

    using MetaMind.Acutance.Parsers.Elements;
    using MetaMind.Engine.Components.Fonts;

    public class Knowledge
    {
        public Knowledge(RawKnowledge rawKnowledge)
            : this(rawKnowledge.Title)
        {
            this.RawKnowledge = rawKnowledge;
        }

        public Knowledge(Title title)
        {
            this.Name = string.Concat(Enumerable.Repeat("#", (int)title.Level)) + " " + title.Name;

            this.IsTitle = true;
        }

        public Knowledge(string name, bool isControl)
        {
            this.Name = name;

            this.IsControl = isControl;
        }

        public bool IsBlank { get; set; }

        public bool IsControl { get; set; }

        public bool IsFile { get; set; }

        public bool IsResult { get; set; }

        public bool IsTitle { get; set; }

        public RawKnowledge RawKnowledge { get; private set; }

        public string Name { get; set; }

        public Command ToCommmand()
        {
            CommandRepetion repetion;

            switch (this.RawKnowledge.Title.Repetition)
            {
                case RepetitionTag.EveryMoment:
                    {
                        repetion = CommandRepetion.EveryMoment;
                    }

                    break;

                case RepetitionTag.EveryDay:
                    {
                        repetion = CommandRepetion.EveryDay;
                    }

                    break;

                case RepetitionTag.EveryWeek:
                    {
                        repetion = CommandRepetion.EveryWeek;
                    }

                    break;

                case RepetitionTag.Unspecified:
                    {
                        repetion = CommandRepetion.Never;
                    }

                    break;

                default:
                    {
                        repetion = CommandRepetion.Never;
                    }

                    break;
            }

            var name = FormatUtils.Compose(this.RawKnowledge.Title.Name);
            return new Command(name, this.RawKnowledge.Path, this.RawKnowledge.Offset, this.RawKnowledge.Title.Time.ToTimeSpan(), repetion);
        }
    }
}