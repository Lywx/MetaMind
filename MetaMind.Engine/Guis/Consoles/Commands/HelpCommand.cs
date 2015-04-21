﻿namespace MonoGameConsole.Commands
{
    using System;
    using System.Linq;
    using System.Text;

    internal class HelpCommand : IConsoleCommand
    {
        public string Name
        {
            get
            {
                return "help";
            }
        }

        public string Description
        {
            get
            {
                return "Displays the list of commands";
            }
        }

        public string Execute(string[] arguments)
        {
            if (arguments != null && arguments.Length >= 1)
            {
                var command = GameConsoleOptions.Commands.Where(c => c.Name != null && c.Name == arguments[0]).FirstOrDefault();
                if (command != null)
                {
                    return string.Format("{0}: {1}\n", command.Name, command.Description);
                }

                return "ERROR: Invalid command '" + arguments[0] + "'";
            }

            var help = new StringBuilder();
            GameConsoleOptions.Commands.Sort(new CommandComparer());
            foreach (var command in GameConsoleOptions.Commands)
            {
                help.Append(string.Format("{0} - ", command.Name).PadRight(12) + string.Format("{0}\n", command.Description));
            }

            return help.ToString();
        }
    }
}