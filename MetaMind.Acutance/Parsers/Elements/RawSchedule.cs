namespace MetaMind.Acutance.Parsers.Elements
{
    using System;

    using MetaMind.Acutance.Concepts;
    using MetaMind.Acutance.Parsers.Grammars;
    using MetaMind.Engine.Components.Fonts;

    using Sprache;

    public class RawSchedule
    {
        public readonly string Content;
        public readonly ScheduleTag Tag;

        public RawSchedule(ScheduleTag tag, string content)
        {
            this.Tag     = tag;
            this.Content = content;
        }

        public Schedule ToSchedule(string path, int offset)
        {
            var command = ScheduleGrammar.CommandUnitParser.Parse(this.Content);

            // name is the part of command unit without identifier
            var name = FormatUtils.Compose(command.Replace("Command: ", string.Empty));
            var time = this.Tag.Time;

            CommandRepetion repetion;
            switch (this.Tag.Repetition)
            {
                case RepetitionTag.EveryMoment:
                    repetion = CommandRepetion.EveryMoment;
                    break;

                case RepetitionTag.EveryDay:
                    repetion = CommandRepetion.EveryDay;
                    break;

                case RepetitionTag.EveryWeek:
                    repetion = CommandRepetion.EveryWeek;
                    break;

                case RepetitionTag.Unspecified:
                    repetion = CommandRepetion.Never;
                    break;

                default:
                    repetion = CommandRepetion.Never;
                    break;
            }

            var now   = DateTime.Now;
            var date  = new DateTime(now.Year, now.Month, now.Day, time.Hours, time.Minutes, time.Seconds);
            var entry = new Schedule(name, path, offset, date, repetion);
            return entry;
        }
    }
}