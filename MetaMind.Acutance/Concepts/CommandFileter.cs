namespace MetaMind.Acutance.Concepts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class CommandFileter
    {
        #region Predicates

        public static Func<Command, bool> IsKnowledge
        {
            get
            {
                return commmand => commmand.Type == CommandType.Knowledge;
            }
        }

        public static Func<Command, bool> IsRunning
        {
            get
            {
                return command => command.State == CommandState.Running;
            }
        }

        public static Func<Command, bool> IsSchedule
        {
            get
            {
                return commmand => commmand.Type == CommandType.Schedule;
            }
        }

        #endregion

        #region Filters

        public static List<Command> RemoveAllShedule(List<Command> commands)
        {
            var schedules = commands.FindAll(new Predicate<Command>(IsSchedule)).ToList();

            foreach (var schedule in schedules)
            {
                commands.Remove(schedule);
            }

            return schedules;
        }

        public static void RemoveRunningShedule(List<Command> commands)
        {
            foreach (var command in commands.Where(IsSchedule).Where(IsRunning).ToArray())
            {
                commands.Remove(command);
            }
        }

        #endregion
    }
}
