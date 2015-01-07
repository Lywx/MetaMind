using System;

namespace MetaMind.Acutance.Concepts
{
    using MetaMind.Acutance.Parsers.Elements;

    public class ScheduleEntry : CommandEntry
    {
        public ScheduleEntry(string name, string path, int offset, DateTime date, CommandRepeativity repeativity)
            : base(name, path, offset, date, repeativity)
        {
            this.Date = date;
        }

        public DateTime Date { get; private set; }
    }
}
