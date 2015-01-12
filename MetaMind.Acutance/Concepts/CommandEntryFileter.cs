using System;
using System.Collections.Generic;

namespace MetaMind.Acutance.Concepts
{
    using System.Linq;

    public static class CommandEntryFileter
    {
        public static Func<CommandEntry, bool> IsKnowledge
        {
            get { return commmand => commmand.Type == CommandType.Knowledge; }
        }

        public static Func<CommandEntry, bool> IsRunning
        {
            get
            {
                return command => command.State == CommandState.Running;
            }
        }

        public static Func<CommandEntry, bool> IsSchedule
        {
            get { return commmand => commmand.Type == CommandType.Schedule; }
        }

        public static List<CommandEntry> RemoveAllShedule(List<CommandEntry> commands)
        {
            var schedules = commands.FindAll(new Predicate<CommandEntry>(IsSchedule)).ToList();

            foreach (var schedule in schedules)
            {
                commands.Remove(schedule);
            }

            return schedules;
        }

        public static void RemoveRunningShedule(List<CommandEntry> commands)
        {
            foreach (var command in commands.Where(IsSchedule).Where(IsRunning).ToArray())
            {
                commands.Remove(command);
            }
        }
    }
}
