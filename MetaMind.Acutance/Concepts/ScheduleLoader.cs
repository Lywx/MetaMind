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

    // FIXME: static ?
    public static class ScheduleLoader
    {
        public static List<Schedule> Load(IConfigurationFileLoader fileLoader)
        {
            var todaySchedules = new List<Schedule>();

            var scheduleFolderPath = LoadScheduleFolderPath(fileLoader);

            try
            {
                var files = Directory.GetFiles(scheduleFolderPath);

                foreach (var file in files.Where(file => Path.GetExtension(file) == ".md"))
                {
                    var scheduleFile = ScheduleGrammar.ScheduleFileParser.Parse(File.ReadAllText(file));

                    // TODO: Need to improve this temporary solution for schedule offset
                    todaySchedules.AddRange(FilterOutToday(scheduleFile.Schedules).ConvertAll(s => s.ToSchedule(file, 0)));
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

        private static List<RawSchedule> FilterOutToday(IEnumerable<RawSchedule> fromFile)
        {
            Predicate<RawSchedule> sameWeekday    = s => (int)s.Tag.Day == (int)DateTime.Now.DayOfWeek;
            Predicate<RawSchedule> repeatEveryday = s => s.Tag.Repetition == RepetitionTag.EveryDay;

            return fromFile.Where(schedule => sameWeekday(schedule) || repeatEveryday(schedule)).ToList();
        }

        private static string LoadScheduleFolderPath(IConfigurationFileLoader fileLoader)
        {
            var dict = ConfigurationFileLoader.LoadUniquePairs(fileLoader);

            var schedulePath = FolderManager.DataPath(dict["ScheduleFolder"]);
            return schedulePath;
        }
    }
}
