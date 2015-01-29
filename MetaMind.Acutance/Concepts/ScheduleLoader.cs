namespace MetaMind.Acutance.Concepts
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using MetaMind.Acutance.Parsers.Elements;
    using MetaMind.Acutance.Parsers.Grammars;
    using MetaMind.Engine.Components;
    using MetaMind.Engine.Settings.Loaders;

    using Sprache;

    public static class ScheduleLoader
    {
        public static List<ScheduleEntry> Load(IConfigurationLoader loader)
        {
            var todaySchedules = new List<ScheduleEntry>();

            var scheduleFolderPath = LoadScheduleFolderPath(loader);

            try
            {
                var files = Directory.GetFiles(scheduleFolderPath);

                foreach (var file in files.Where(file => Path.GetExtension(file) == ".md"))
                {
                    var scheduleFile = ScheduleGrammar.ScheduleFileParser.Parse(File.ReadAllText(file));

                    // TODO: Need to improve this temporary solution for schedule offset
                    todaySchedules.AddRange(FilterOutToday(scheduleFile.Schedules).ConvertAll(s => s.ToEntry(file, 0)));
                }

                return todaySchedules;
            }
            catch (ArgumentNullException)
            {
                // when cannot determine schedule folder in settings
                return todaySchedules;
            }
            catch (DirectoryNotFoundException)
            {
                // when cannot find specified schedule folder
                return todaySchedules;
            }
        }

        private static List<Schedule> FilterOutToday(IEnumerable<Schedule> fromFile)
        {
            Predicate<Schedule> sameWeekday    = s => (int)s.Date.Day == (int)DateTime.Now.DayOfWeek;
            Predicate<Schedule> repeatEveryday = s => s.Date.Repeativity == RepeativityTag.EveryDay;

            return fromFile.Where(schedule => sameWeekday(schedule) || repeatEveryday(schedule)).ToList();
        }

        private static string LoadScheduleFolderPath(IConfigurationLoader loader)
        {
            var dict = ConfigurationLoader.LoadUniquePairs(loader);

            var schedulePath = FolderManager.DataPath(dict["ScheduleFolder"]);
            return schedulePath;
        }
    }
}
