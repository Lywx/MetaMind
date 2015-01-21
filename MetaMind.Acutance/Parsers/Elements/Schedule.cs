namespace MetaMind.Acutance.Parsers.Elements
{
    using System;

    using MetaMind.Acutance.Concepts;
    using MetaMind.Acutance.Parsers.Grammars;

    using Sprache;

    public static class ScheduleExtension
    {
        public static ScheduleEntry ToEntry(this Schedule schedule, string path, int offset)
        {
            var command = ScheduleGrammar.CommandUnitParser.Parse(schedule.Content);

            // name is the part of command unit without identifier
            var name = command.Replace("Command: ", string.Empty);
            var time = schedule.Date.Time;

            CommandRepeativity repeativity;
            switch (schedule.Date.Repeativity)
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

    public class Schedule
    {
        public readonly string Content;
        public readonly DateTag Date;

        public Schedule(DateTag date, string content)
        {
            this.Date = date;
            this.Content = content;
        }
    }
}