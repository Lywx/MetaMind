using System;

namespace MetaMind.Acutance.Concepts
{
    /// <remarks>
    /// Date of schedule should not be in side 13:55, 28:55, 43:55, 58:55 
    /// which will collide with save manager save listener in MultiplexerGroup.
    /// </remarks>
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
