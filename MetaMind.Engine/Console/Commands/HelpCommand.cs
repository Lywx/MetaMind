namespace MetaMind.Engine.Console.Commands
{
    using System;
    using System.Linq;
    using System.Text;

    internal class HelpCommand : IConsoleCommand
    {
        private readonly GameConsole console;

        public HelpCommand(GameConsole console)
        {
            if (console == null)
            {
                throw new ArgumentNullException(nameof(console));
            }

            this.console = console;
        }

        public string Name => "help";

        public string Description => "Displays the command description";

        public string Execute(string[] arguments)
        {
            if (arguments != null && arguments.Length >= 1)
            {
                var command = this.console.Commands.FirstOrDefault(
                        c => c.Name != null && 
                        c.Name == arguments[0]);

                if (command != null)
                {
                    return $"{command.Name}: {command.Description}\n";
                }

                return "ERROR: Invalid command '" + arguments[0] + "'";
            }

            this.console.Commands.Sort(new CommandComparer());

            var padding = this.console.Commands.Max(command => command.Name.Length);

            var help = new StringBuilder();
            foreach (var command in this.console.Commands)
            {
                help.Append(command.Name.PadRight(padding) + " - " + $"{command.Description}\n");
            }

            return help.ToString();
        }
    }
}