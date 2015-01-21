namespace MetaMind.Acutance.Concepts
{
    using MetaMind.Acutance.Parsers.Elements;
    using System.Linq;

    public class KnowledgeEntry
    {
        public KnowledgeEntry(Knowledge knowledge)
        {
            if (knowledge != null)
            {
                this.Knowledge = knowledge;

                this.Name = string.Concat(Enumerable.Repeat("#", (int)knowledge.Title.Level)) + " " + knowledge.Title.Name;

                this.IsTitle = true;
            }
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

            return new CommandEntry(this.Knowledge.Title.Name, this.Knowledge.Path, this.Knowledge.Offset, this.Knowledge.Title.Time.ToTimeSpan(), repeativity);
        }
    }
}