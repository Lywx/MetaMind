namespace MetaMind.Acutance.Parsers.Elements
{
    using System;

    using MetaMind.Acutance.Concepts;
    using MetaMind.Acutance.Parsers.Grammars;
    using MetaMind.Engine.Components.Fonts;

    using Sprache;

    public class Schedule
    {
        public readonly string  Content;
        public readonly DateTag Date;

        public Schedule(DateTag date, string content)
        {
            this.Date    = date;
            this.Content = content;
        }

        public ScheduleEntry ToEntry(string path, int offset)
        {
            var command = ScheduleGrammar.CommandUnitParser.Parse(this.Content);

            // name is the part of command unit without identifier
            var name = Format.Compose(command.Replace("Command: ", string.Empty), 10, "", "> ", "", "");
            var time = this.Date.Time;

            CommandRepeativity repeativity;
            switch (this.Date.Repeativity)
            {
                case RepeativityTag.EveryMoment:
                    repeativity = CommandRepeativity.EveryMoment;
                    break;

                case RepeativityTag.EveryDay:
                    repeativity = CommandRepeativity.EveryDay;
                    break;

                case RepeativityTag.EveryWeek:
                    repeativity = CommandRepeativity.EveryWeek;
                    break;

                case RepeativityTag.Unspecified:
                    repeativity = CommandRepeativity.Never;
                    break;

                default:
                    repeativity = CommandRepeativity.Never;
                    break;
            }

            var now   = DateTime.Now;
            var date  = new DateTime(now.Year, now.Month, now.Day, time.Hours, time.Minutes, time.Seconds);
            var entry = new ScheduleEntry(name, path, offset, date, repeativity);
            return entry;
        }
    }
}