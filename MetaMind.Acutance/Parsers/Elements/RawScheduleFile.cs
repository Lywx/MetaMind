namespace MetaMind.Acutance.Parsers.Elements
{
    using System.Collections.Generic;

    public class RawScheduleFile
    {
        public readonly List<RawSchedule> Schedules;

        public RawScheduleFile(List<RawSchedule> schedules)
        {
            this.Schedules = schedules;
        }
    }
}