namespace MetaMind.Acutance.Parsers.Elements
{
    using System.Collections.Generic;

    public class ScheduleFile
    {
        public readonly List<Schedule> Schedules;

        public ScheduleFile(List<Schedule> schedules)
        {
            this.Schedules = schedules;
        }
    }
}