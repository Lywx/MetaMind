namespace MetaMind.Acutance.Concepts
{
    using System.Collections.Generic;
    using System.Linq;

    using MetaMind.Acutance.Parsers.Elements;
    using MetaMind.Engine.Components.Fonts;

    public class KnowledgeEntry
    {
        public KnowledgeEntry(Knowledge knowledge)
            : this(knowledge.Title)
        {
            this.Knowledge = knowledge;
        }

        public KnowledgeEntry(Title title)
        {
            this.Name = string.Concat(Enumerable.Repeat("#", (int)title.Level)) + " " + title.Name;

            this.IsTitle = true;
        }

        public KnowledgeEntry(string name, bool isControl)
        {
            this.Name = name;

            this.IsControl = isControl;
        }

        public bool IsBlank { get; set; }

        public bool IsControl { get; set; }

        public bool IsFile { get; set; }

        public bool IsResult { get; set; }

        public bool IsTitle { get; set; }

        public Knowledge Knowledge { get; private set; }

        public string Name { get; set; }

        public CommandEntry ToCommmandEntry()
        {
            CommandRepeativity repeativity;

            switch (this.Knowledge.Title.Repeativity)
            {
                case RepeativityTag.EveryMoment:
                    {
                        repeativity = CommandRepeativity.EveryMoment;
                    }

                    break;

                case RepeativityTag.EveryDay:
                    {
                        repeativity = CommandRepeativity.EveryDay;
                    }

                    break;

                case RepeativityTag.EveryWeek:
                    {
                        repeativity = CommandRepeativity.EveryWeek;
                    }

                    break;

                case RepeativityTag.Unspecified:
                    {
                        repeativity = CommandRepeativity.Never;
                    }

                    break;

                default:
                    {
                        repeativity = CommandRepeativity.Never;
                    }

                    break;
            }

            var name = Format.Compose(this.Knowledge.Title.Name, 10, "", "> ", "", "");
            return new CommandEntry(name, this.Knowledge.Path, this.Knowledge.Offset, this.Knowledge.Title.Time.ToTimeSpan(), repeativity);
        }
    }
}